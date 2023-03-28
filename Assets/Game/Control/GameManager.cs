using Assets.Game.Objects.Crewmates;
using Assets.Game.Objects.Hunts;
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
        [SerializeField] private Hunt hunt;
        [SerializeField] private Transform airlock;

        private Crewmate potentialCrewmate;

        private bool waitingForUser;

        public void Awake()
        {
            ui.crewmateAccept += AcceptCrewmateCallback;
            ui.crewmateRefuse += RefuseCrewmateCallback;
            ui.crewmateNew += NewCrewmateCallback;
        }

        public void Start()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            ui.SetNumInRoster(stats.NumInRoster);
            ui.SetNumHunts(stats.NumHunts);
            ui.SetNumDeaths(stats.NumDeaths);
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

                // Change sprite
                parasite.SwapSprite();

                MoveToAirlock(parasite);

                BeginHunt(parasite);
            }

            // Is the game over?
            CheckWinLoss();

            // Let player create a new one
            waitingForUser = false;
        }

        private void MoveToAirlock(ParasiteCrewmate parasite)
        {
            // Move to airlock
            parasite.transform.SetParent(airlock);
            parasite.transform.localScale = new Vector3(0.5F, 0.5F, 1);

            // Destination position
            Vector3 destination = airlock.position + new Vector3(stats.NumHunts, 0, 0);
            parasite.SetDestination(destination, 1);
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
                ui.AppendToLog($"...and good thing too. As {potentialCrewmate} returns to their ship you see their visage blur and fade. " +
                    $"{potentialCrewmate} was a parasite!");
            }

            // Destroy this
            Destroy(potentialCrewmate.gameObject);

            // Let player create a new one
            waitingForUser = false;
        }

        private void GeneratePotentialCrewmate()
        {
            // Create a new potential crewmate
            potentialCrewmate = factory.Create();

            // Update ui with details of this crewmate
            ui.SetPotentialCrewmateDetails(potentialCrewmate.name, potentialCrewmate.Hobby);
        }

        private void CheckWinLoss()
        {
            // Yay, full roster
            if (roster.IsFull())
            {
                WinGame();
            }

            // Too many parasites
            if (stats.NumHunts >= 5)
            {
                LoseGame();
            }

            // Too many deaths
            if (stats.NumDeaths >= 5)
            {
                LoseGame();
            }
        }

        private void LoseGame()
        {
            ui.ClearPotentialCrewmateDetails();
            ui.ClearLog();

            ui.AppendToLog("You have been fired as the head of HR");
            ui.AppendToLog($"You let {stats.NumHunts} parasites onto the ship, killing {stats.NumDeaths} people!");
            ui.AppendToLog("Game Over");

            // Disable all buttons
            DisableButtons();
        }

        private void DisableButtons()
        {
            ui.crewmateAccept -= AcceptCrewmateCallback;
            ui.crewmateRefuse -= RefuseCrewmateCallback;
            ui.crewmateNew -= NewCrewmateCallback;

            ui.DisableButtons();
        }

        private void WinGame()
        {
            ui.ClearPotentialCrewmateDetails();
            ui.ClearLog();

            ui.AppendToLog("Nice one, a full crew!");
            ui.AppendToLog($"And only {stats.NumDeaths} people had to die. Well done!");
            ui.AppendToLog("Game Over");

            // Disable all buttons
            DisableButtons();
        }

        private void AddCrewmate(HumanCrewmate crewmate)
        {
            // Add to roster
            roster.Add(crewmate);

            // Move image to roster location
            MoveToRoster(crewmate);

            // Record stats
            stats.IncrementNumInRoster();
            ui.SetNumInRoster(stats.NumInRoster);
        }

        private void MoveToRoster(HumanCrewmate crewmate)
        {
            // Move to roster
            crewmate.transform.SetParent(roster.transform);

            // Destination position
            Vector3 destination = roster.transform.position + new Vector3(stats.NumInRoster, 0, 0);
            crewmate.SetDestination(destination, 1);
        }

        private void BeginHunt(ParasiteCrewmate hunter)
        {
            // Start the hunt
            ISet<HumanCrewmate> allPrey = hunter.GoHunting();

            // Kill all the prey
            int i = 0;
            foreach (HumanCrewmate p in allPrey)
            {
                // No longer in the roster
                roster.Remove(p);

                // Dies
                p.Die();

                // Move the image to the Hunt location
                MoveToHunt(p, i);

                i++;
            }

            ui.AppendToLog($"The following crewmates were killed: {string.Join(", ", allPrey)}");

            // Parasite escapes
            //hunter.Escape();
            ui.AppendToLog($"{potentialCrewmate} has escaped into space after his killing spree");

            // Update stats and UI
            stats.IncrementNumHunts();
            stats.AddNumDeaths(allPrey.Count);
            stats.SubtractNumInRoster(allPrey.Count);
            UpdateUI();
        }

        private void MoveToHunt(HumanCrewmate p, int index)
        {
            // Move to hunt
            p.transform.SetParent(hunt.transform);

            // Destination position
            Vector3 destination = hunt.transform.position + new Vector3(stats.NumDeaths + index, 0, 0);
            p.SetDestination(destination, 1);
        }
    }
}
