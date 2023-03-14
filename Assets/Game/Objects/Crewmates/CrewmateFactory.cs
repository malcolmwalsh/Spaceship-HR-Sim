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
            ICrewmate crewmate;

            if (IsParasite()) {
                crewmate = Instantiate(_parasiteCrewmate);

                // Randomly find and assign a hobby
                crewmate.Hobby = _parasiteHobbyPool.ChooseOne();

            } else {
                crewmate = Instantiate(_humanCrewmate);

                // Randomly find and assign a hobby
                crewmate.Hobby = _humanHobbyPool.ChooseOne();
            }

            // Pick a name at random
            crewmate.Name = _namePool.ChooseOne();

            return crewmate;
        }

        private bool IsParasite()
        {
            // Random chance using CHANCE_IS_PARASITE
            return UnityEngine.Random.value < CHANCE_IS_PARASITE;
        }
    }
}
