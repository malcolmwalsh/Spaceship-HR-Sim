using Assets.Game.Objects.Hunts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class ParasiteCrewmate : Crewmate
    {
        [SerializeField] private Hunt Hunt;
        [SerializeField] private Sprite parasiteSprite;
 
        public ISet<HumanCrewmate> GoHunting()
        {
            return Hunt.BeginHunt();
        }

        public void Escape()
        {
            Destroy(this.gameObject);
        }

        public override string ToString()
        {
            return name;
        }

        public void SwapSprite()
        {
            GetComponent<SpriteRenderer>().sprite = parasiteSprite;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
