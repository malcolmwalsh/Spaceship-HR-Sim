using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class HumanCrewmate : Crewmate
    {
        public void Die()
        {
            // Death

            // Change colour
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
