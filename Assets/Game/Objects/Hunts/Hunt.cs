using Assets.Game.Objects.Crewmates;
using System;
using System.Collections.Generic;
using Assets.Game.Objects.Rosters;
using UnityEngine;

namespace Assets.Game.Objects.Hunts
{
    public class Hunt
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] private int MIN_PREY_TO_KILL;
        // ReSharper disable once InconsistentNaming
        [SerializeField] private int MAX_PREY_TO_KILL;

        [SerializeField] private Roster _preyPopulation;

        public IList<IPrey> BeginHunt(IHunter hunter)
        {
            //GetNumberToKill(MIN_PREY_TO_KILL, MAX_PREY_TO_KILL)->numToKill
            //for i = 1 : numToKill
            //preyPopulation.GetItem(i)->preyToKill
            //allPrey.Add(preyToKill)
            //return allPrey

            throw new NotImplementedException();
        }

        private int GetNumberToKill()
        {
            //Random.Range
            //Check number not greater than prey population size
            //MIN(preyPopulation.Count, MAX_PREY_TO_KILL)

            throw new NotImplementedException();
        }

    }
}
