namespace Assets.Game.Control
{
    public class StatController
    {
        private int _numDeaths;
        private int _numHunts;
        private int _numinRoster;

        public void IncrementNumHunts()
        {
            _numHunts++;
        }

        public void AddNumDeaths(int value)
        {
            _numDeaths = +value;
        }

        public void IncrementNumInRoster()
        {
            _numinRoster++;
        }

        public void SubtractNumInRoster(int value)
        {
            _numinRoster -= value;
        }
    }
}
