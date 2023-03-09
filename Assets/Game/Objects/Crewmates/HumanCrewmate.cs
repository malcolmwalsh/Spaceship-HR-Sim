using UnityEngine;

namespace Assets.Game.Objects.Crewmates
{
    public class HumanCrewmate : MonoBehaviour, ICrewmate, IPrey
    {
        public string Name { get; set; }
        public string Hobby { get; set; }
        
        public void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}
