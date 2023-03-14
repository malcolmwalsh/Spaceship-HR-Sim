using Assets.Game.Objects.Crewmates;
using Assets.Game.Objects.Rosters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Control
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UI ui;
        [SerializeField] private Roster roster;
        [SerializeField] private CrewmateFactory factory;
        [SerializeField] private StatController stats;

        private ICrewmate potentialCrewmate;

        private bool waitingForUser;

        public void Awake()
        {
            ui.crewmateAccept += AcceptCrewmateCallback;
            ui.crewmateRefuse += RefuseCrewmateCallback;
            ui.crewmateNew += NewCrewmateCallback;
        }

        public void Start()
        {
            ui.SetNumInRoster(stats.NumInRoster);
        }

        private void NewCrewmateCallback(object sender, EventArgs e)
        {
            if (!waitingForUser)
            {
                waitingForUser = true;

                // Clear log
                ui.ClearLog();

                // Generate a new one
                GeneratePotentialCrewmate();
            }
        }

        private void AcceptCrewmateCallback(object sender, EventArgs e)
        {
            AcceptCrewmate();
        }

        private void AcceptCrewmate()
        {           
            // Show the result of the choice
            ui.AppendToLog($"Crewmate {potentialCrewmate} accepted");

            if (potentialCrewmate is HumanCrewmate human)
            {
                // Is human
                ui.AppendToLog($"{potentialCrewmate} is a perfectly normal human");

                AddCrewmate(human);
            }
            else if (potentialCrewmate is ParasiteCrewmate parasite)
            {
                // Is parasite
                ui.AppendToLog($"{potentialCrewmate} is a parasite! The crew is in mortal danger now.");

                BeginHunt(parasite);
            }

            // Is the game over?
            CheckWinLoss();

            // Let player create a new one
            waitingForUser = false;
        }

        private void RefuseCrewmateCallback(object sender, EventArgs e)
        {
            RefuseCrewmate();
        }

        private void RefuseCrewmate()
        {
            ui.AppendToLog($"You refuse to let {potentialCrewmate} near the crew");

            if (potentialCrewmate is ParasiteCrewmate)
            {
                ui.AppendToLog($"...and good thing too. As {potentialCrewmate} returns to his ship you see his visage blur and fade. " +
                    $"{potentialCrewmate} was a parasite!");
            }

            // Let player create a new one
            waitingForUser = false;
        }

        private void GeneratePotentialCrewmate()
        {
            // Create a new potential crewmate
            potentialCrewmate = factory.Create();

            // Update ui with details of this crewmate
            ui.SetPotentialCrewmateDetails(potentialCrewmate.Name, potentialCrewmate.Hobby);
        }

        private void CheckWinLoss()
        {
            if (roster.IsFull())
            {
                WinGame();
            }

            // TODO - Not sure about this
            //if (roster.IsEmpty())
            //{
            //    LoseGame();
            //} 
        }

        private void LoseGame()
        {
            throw new NotImplementedException();
        }

        private void WinGame()
        {
            throw new NotImplementedException();
        }

        private void AddCrewmate(ICrewmate crewmate)
        {
            roster.Add(crewmate);

            stats.IncrementNumInRoster();
            ui.SetNumInRoster(stats.NumInRoster);
        }

        private void BeginHunt(IHunter hunter)
        {
            //(List<IPrey>)potentialCrewmate.GoHunting->allPrey
            //foreach in allPrey
            //prey.Die
            //ui.AppentToLog($"The following crewmates were killed: {allPrey}")
            //potentialCrewmate.Escape
            //ui.AppentToLog($"{potentialCrewmate} has escaped into space after his killing spree")
            //stats.IncrementNumHunts()
            //stats.SubtractNumInRoster(allPrey.Count)
            //ui.SetNumHunts(int value = 1)
            //ui.SetNumDeaths(int value)
            //ui.SetNumInRoster(roster.Count)

            // Start the hunt
            IList<IPrey> allPrey = hunter.GoHunting();

            // Kill all the prey
            foreach(IPrey p in allPrey)
            {
                // TODO This could lead to parallel error
                p.Die();
            }
        }
    }
}
