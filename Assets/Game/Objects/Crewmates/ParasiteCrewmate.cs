using Assets.Game.Objects.Hunts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class ParasiteCrewmate : Crewmate, IHunter
    {
        [SerializeField] private Hunt Hunt;

        public ISet<IPrey> GoHunting()
        {
            return Hunt.BeginHunt(this);
        }

        public void Escape()
        {
            Destroy(this);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
