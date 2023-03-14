using Assets.Game.Objects.Hunts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class ParasiteCrewmate : MonoBehaviour, ICrewmate, IHunter
    {
        public string Name { get; set; }
        public string Hobby { get; set; }

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
            return Name;
        }
    }
}
