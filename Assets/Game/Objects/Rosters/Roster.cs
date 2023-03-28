using System.Collections.Generic;
using Assets.Game.Extensions;
using Assets.Game.Objects.Crewmates;
using UnityEngine;

namespace Assets.Game.Objects.Rosters
{
    public class Roster : MonoBehaviour, IRoster
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] private int MAX_ROSTER_SIZE;

        private readonly List<HumanCrewmate> _crewmates = new List<HumanCrewmate>();

        public HumanCrewmate ChooseOne()
        {
            return _crewmates.RandomElement();
        }

        public void Add(HumanCrewmate newCrewmate)
        {
            if (IsFull())
            {
                throw new System.ArgumentOutOfRangeException("Cannot add another crew mate as roster is full");
            }

            _crewmates.Add(newCrewmate);
        }

        public void Remove(HumanCrewmate crewmate)
        {
            if (IsEmpty())
            {
                throw new System.ArgumentOutOfRangeException("Cannot remove crew mate as roster is empty");
            }

            _crewmates.Remove(crewmate);
        }

        public int Count()
        {
            return _crewmates.Count;
        }

        public bool IsFull()
        {
            return Count() >= MAX_ROSTER_SIZE;
        }

        public bool IsEmpty()
        {
            return Count() <= 0;
        }
    }
}
