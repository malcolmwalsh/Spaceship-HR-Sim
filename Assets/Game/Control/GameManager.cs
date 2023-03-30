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

        private Crewmate _potentialCrewmate;

        private Canvas _canvas;
        private Transform _crewLineUp;
        private Transform _deadLineUp;
        private Transform _parasiteLineUp;


        public void Awake()
        {
            RegisterNewCrewmateCallback();
        }

        public void Start()
        {
            // Try to find the Canvas
            GameObject tempObject = GameObject.Find("Canvas");
            if (tempObject != null)
            {
                // Get the Canvas component
                _canvas = tempObject.GetComponent<Canvas>();
                if (_canvas == null)
                {
                    throw new ApplicationException("Could not locate Canvas component on " + tempObject.name);
                }

                _crewLineUp = _canvas.transform.Find("LineUps").Find("Crew LineUp");
                _deadLineUp = _canvas.transform.Find("LineUps").Find("Dead LineUp");
                _parasiteLineUp = _canvas.transform.Find("LineUps").Find("Parasite LineUp");

            }


            UpdateUI();
        }

        private void RegisterNewCrewmateCallback()
        {
            ui.crewmateNew += NewCrewmateCallback;
        }

        private void DeregisterNewCrewmateCallback()
        {
            ui.crewmateNew -= NewCrewmateCallback;
        }

        private void RegisterAcceptRefuseCrewmateCallback()
        {
            ui.crewmateAccept += AcceptCrewmateCallback;
            ui.crewmateRefuse += RefuseCrewmateCallback;
        }

        private void DeregisterAcceptRefuseCrewmateCallback()
        {
            ui.crewmateAccept -= AcceptCrewmateCallback;
            ui.crewmateRefuse -= RefuseCrewmateCallback;
        }

        private void UpdateUI()
        {
            ui.SetNumInRoster(stats.NumInRoster);
            ui.SetNumHunts(stats.NumHunts);
            ui.SetNumDeaths(stats.NumDeaths);
        }

        private void NewCrewmateCallback(object sender, EventArgs e)
        {
            // Deregister and register events
            DeregisterNewCrewmateCallback();
            RegisterAcceptRefuseCrewmateCallback();

            // Clear log
            ui.ClearLog();

            // Generate a new one
            GeneratePotentialCrewmate();            
        }

        private void AcceptCrewmateCallback(object sender, EventArgs e)
        {
            // Deregister and register events
            RegisterNewCrewmateCallback();
            DeregisterAcceptRefuseCrewmateCallback();

            AcceptCrewmate();
        }

        private void AcceptCrewmate()
        {           
            // Show the result of the choice
            ui.AppendToLog($"Crewmate {_potentialCrewmate} accepted");

            if (_potentialCrewmate is HumanCrewmate human)
            {
                // Is human
                ui.AppendToLog($"{_potentialCrewmate} is a perfectly normal human");

                AddCrewmate(human);
            }
            else if (_potentialCrewmate is ParasiteCrewmate parasite)
            {
                // Is parasite
                ui.AppendToLog($"{_potentialCrewmate} is a parasite! The crew is in mortal danger now.");

                // Change sprite
                parasite.SwapSprite();

                MoveToAirlock(parasite);

                BeginHunt(parasite);
            }

            // Is the game over?
            CheckWinLoss();
        }

        private void MoveToAirlock(ParasiteCrewmate parasite)
        {
            // Move to airlock
            parasite.transform.SetParent(airlock);
            parasite.transform.localScale = new Vector3(0.5F, 0.5F, 1);

            // Destination position
            Vector3 destination = _parasiteLineUp.transform.position + new Vector3(stats.NumHunts, 0, 0);
            parasite.SetDestination(destination, 1);
        }

        private void RefuseCrewmateCallback(object sender, EventArgs e)
        {
            // Deregister and register events
            RegisterNewCrewmateCallback();
            DeregisterAcceptRefuseCrewmateCallback();

            RefuseCrewmate();
        }

        private void RefuseCrewmate()
        {
            ui.AppendToLog($"You refuse to let {_potentialCrewmate} near the crew");

            if (_potentialCrewmate is ParasiteCrewmate)
            {
                ui.AppendToLog($"...and good thing too. As {_potentialCrewmate} returns to their ship you see their visage blur and fade. " +
                    $"{_potentialCrewmate} was a parasite!");
            }

            // Destroy this
            Destroy(_potentialCrewmate.gameObject);
        }

        private void GeneratePotentialCrewmate()
        {
            // Create a new potential crewmate
            _potentialCrewmate = factory.Create();

            // Update ui with details of this crewmate
            ui.SetPotentialCrewmateDetails(_potentialCrewmate.name, _potentialCrewmate.Hobby);
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
            DeregisterNewCrewmateCallback();
            DeregisterAcceptRefuseCrewmateCallback();

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
            Vector3 destination = _crewLineUp.transform.position + new Vector3(stats.NumInRoster, 0, 0);
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
            ui.AppendToLog($"{_potentialCrewmate} has escaped into space after his killing spree");

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
            Vector3 destination = _deadLineUp.transform.position + new Vector3(stats.NumDeaths + index, 0, 0);
            p.SetDestination(destination, 1);
        }
    }
}
