using System;
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

        public void SetPotentialCrewmateDetails(string name, string hobby) {
            potentialCrewmateDetails.SetText($"The newest recruit's name is {name} and their favourite hobby is {hobby}");
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
            crewmateAccept.Invoke(this, EventArgs.Empty);
        }

        public void OnClickRefuseCrewMate()
        {
            crewmateRefuse.Invoke(this, EventArgs.Empty);
        }

        public void OnClickNewCrewMate()
        {
            crewmateNew.Invoke(this, EventArgs.Empty);
        }

        public void AppendToLog(string text)
        {
            log.SetText(text);
        }

        public void ClearLog()
        {
            // There must be a better way
            log.SetText("");
        }
    }
}
