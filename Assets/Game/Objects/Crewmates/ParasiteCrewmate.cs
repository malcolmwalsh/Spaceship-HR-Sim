using System;
using System.Collections.Generic;
using Assets.Game.Objects.Hunts;
using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class ParasiteCrewmate : MonoBehaviour, ICrewmate, IHunter
    {
        public string Name { get; set; }
        public string Hobby { get; set; }
        public Hunt Hunt { get; set; }

        public IList<IPrey> GoHunting()
        {
            return Hunt.BeginHunt(this);
        }

        public void Escape()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
