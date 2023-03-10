using Assets.Game.Objects.Crewmates;
using Assets.Game.Objects.Rosters;
using Newtonsoft.Json.Linq;
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

        public void Awake()
        {
            ui.crewmateAccept += AcceptCrewmateCallback;
            ui.crewmateRefuse += RefuseCrewmateCallback;
        }

        public void Start()
        {
            GeneratePotentialCrewmate();
        }


        public void Update()
        {
        
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
                AddCrewmate(human);
            }
            else if (potentialCrewmate is ParasiteCrewmate parasite)
            {
                // Is parasite
                BeginHunt(parasite);
            }

            //if crewmate is IPrey, 
            //AddCrewmate(potentialCrewmate)
            //else
            //                BeginHunt(potentialCrewmate)
            //CheckWinLoss()
            //GeneratePotentialCrewmate

            throw new NotImplementedException();
        }

        private void RefuseCrewmateCallback(object sender, EventArgs e)
        {
            RefuseCrewmate();
        }

        private void RefuseCrewmate()
        {
            throw new NotImplementedException();
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
            //if roster.IsFull then win
            //if roster.IsEmpty then lose?

            throw new NotImplementedException();
        }

        private void AddCrewmate(ICrewmate crewmate)
        {
            roster.Add(crewmate);
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
