using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Game.Objects.Crewmates
{
    public abstract class Crewmate : MonoBehaviour
    {
        float t;
        Vector3 startPosition;
        Vector3 targetPosition;
        float timeToReachTarget;

        public string Hobby { get; set; }

        void Start()
        {
            startPosition = targetPosition = transform.position;
        }

        public void Update()
        {
            if (!startPosition.Equals(targetPosition))
            {
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            }
        }

        public void SetDestination(Vector3 destination, float time)
        {
            t = 0;
            startPosition = transform.position;
            timeToReachTarget = time;
            targetPosition = destination;
        }
    }
}
