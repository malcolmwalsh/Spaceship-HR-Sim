using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Game.Objects.Crewmates
{
    public abstract class Crewmate : MonoBehaviour
    {
        private float _t;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _timeToReachTarget;

        public string Hobby { get; set; }

        void Start()
        {
            _startPosition = _targetPosition = transform.position;
        }

        public void Update()
        {
            if (!_startPosition.Equals(_targetPosition))
            {
                _t += Time.deltaTime / _timeToReachTarget;
                transform.position = Vector3.Lerp(_startPosition, _targetPosition, _t);
            }
        }

        public void SetDestination(Vector3 destination, float time)
        {
            _t = 0;
            _startPosition = transform.position;
            _timeToReachTarget = time;
            _targetPosition = destination;
        }
    }
}
