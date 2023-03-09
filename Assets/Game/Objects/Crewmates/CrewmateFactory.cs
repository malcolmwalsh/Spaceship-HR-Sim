using System;
using Assets.Game.Objects.OptionPools;
using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class CrewmateFactory : MonoBehaviour, IFactory<ICrewmate>
    {
        [SerializeField] private HumanCrewmate _humanCrewmate;
        [SerializeField] private ParasiteCrewmate _parasiteCrewmate;
        [SerializeField] private OptionPool _namePool;
        [SerializeField] private OptionPool _humanHobbyPool;
        [SerializeField] private OptionPool _parasiteHobbyPool;

        // ReSharper disable once InconsistentNaming
        [SerializeField] private float CHANCE_IS_PARASITE;

        public ICrewmate Create()
        {

            //namePool.ChooseOne() -> name
            
            //If IsParasite()
            //Instantiate(parasite);
            //parasiteHobbyPool.ChooseOne() -> hobby
            //newCrewmate.SetName(name)
            //newCrewmate.SetHobby(hobby)
            
            //Else
            //Instantiate(human)
            //humanHobbyPool.ChooseOne() -> hobby
            //newCrewmate.SetName(name)
            //newCrewmate.SetHobby(hobby)
            
            //return newCrewmate

            throw new NotImplementedException();
        }

        private bool IsParasite()
        {
            // Random chance using CHANCE_IS_PARASITE

            throw new NotImplementedException();
        }
    }
}
