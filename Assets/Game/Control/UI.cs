using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Game.Control
{
    public class UI : MonoBehaviour, IInput, IOutput
    {
        public event EventHandler crewmateAccept;
        public event EventHandler crewmateRefuse;
        public event EventHandler crewmateNew;

        [SerializeField] private TMP_Text potentialCrewmateDetails;
        [SerializeField] private TMP_Text numInRoster;
        [SerializeField] private TMP_Text numHunts;
        [SerializeField] private TMP_Text numDeaths;
        [SerializeField] private TMP_Text log;

        LinkedList<string> logEntries = new LinkedList<string>();
        private const int NUM_LOG_ENTRIES = 3;

        public void SetPotentialCrewmateDetails(string name, string hobby) {
            potentialCrewmateDetails.SetText($"The potential recruit's name is {name} and their favourite hobby is {hobby}");
        }

        public void ClearPotentialCrewmateDetails()
        {
            potentialCrewmateDetails.SetText("");
        }

        public void SetNumInRoster(int value) {
            numInRoster.SetText(value.ToString());
        }

        public void SetNumHunts(int value) {
            numHunts.SetText(value.ToString());
        }

        public void SetNumDeaths(int value) {
            numDeaths.SetText(value.ToString());
        }

        public void OnClickAcceptCrewMate() {
            crewmateAccept?.Invoke(this, EventArgs.Empty);
        }

        public void OnClickRefuseCrewMate()
        {
            crewmateRefuse?.Invoke(this, EventArgs.Empty);
        }

        public void OnClickNewCrewMate()
        {
            crewmateNew?.Invoke(this, EventArgs.Empty);
        }

        public void AppendToLog(string text)
        {
            AppendLogEntries(text);

            PrintLog(string.Join("\n", logEntries));
        }

        private void AppendLogEntries(string text)
        {
            if (logEntries.Count >= NUM_LOG_ENTRIES)
            {
                logEntries.RemoveFirst();
            }
            logEntries.AddLast(text);
        }

        private void ClearLogEntries()
        {
            logEntries.Clear();
        }

        public void ClearLog()
        {
            ClearLogEntries();

            // There must be a better way
            PrintLog("");
        }

        private void PrintLog(string text)
        {
            log.SetText(text);
        }
    }
}
