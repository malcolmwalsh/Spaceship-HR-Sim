using Assets.Game.Objects.Crewmates;
using System;
using System.Collections.Generic;
using Assets.Game.Objects.Rosters;
using UnityEngine;

namespace Assets.Game.Objects.Hunts
{
    public class Hunt : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        [SerializeField] private int MIN_PREY_TO_KILL;
        // ReSharper disable once InconsistentNaming
        [SerializeField] private int MAX_PREY_TO_KILL;

        [SerializeField] private Roster _preyPopulation;

        public ISet<IPrey> BeginHunt(IHunter hunter)
        {
            // Store all the prey
            ISet<IPrey> allPrey = new HashSet<IPrey>();

            // The number to kill
            int numberToKill = GetNumberToKill();

            // Keep going until we've found them all
            while(allPrey.Count < numberToKill)
            {
                // Pick a crewmate to kill
                IPrey preyToKill = (IPrey)_preyPopulation.ChooseOne();  // This cast makes me sad
                
                // Save it
                allPrey.Add(preyToKill);
            }           
            
            return allPrey;
        }

        private int GetNumberToKill()
        {
            // Sample
            int numberToKill = UnityEngine.Random.Range(MIN_PREY_TO_KILL, MAX_PREY_TO_KILL);

            // Ensure sensible
            numberToKill = Math.Min(numberToKill, _preyPopulation.Count());

            return numberToKill;
        }

    }
}
