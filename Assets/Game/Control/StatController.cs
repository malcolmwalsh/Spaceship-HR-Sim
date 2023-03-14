using UnityEngine;

namespace Assets.Game.Control
{
    public class StatController : MonoBehaviour
    {
        private int _numDeaths;
        private int _numHunts;
        private int _numInRoster;

        public int NumDeaths { get => _numDeaths; }
        public int NumHunts { get => _numHunts; }
        public int NumInRoster { get => _numInRoster; }

        public void IncrementNumHunts()
        {
            _numHunts++;
        }

        public void AddNumDeaths(int value)
        {
            _numDeaths += value;
        }

        public void IncrementNumInRoster()
        {
            _numInRoster++;
        }

        public void SubtractNumInRoster(int value)
        {
            _numInRoster -= value;
        }
    }
}
