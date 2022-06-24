using UnityEngine;
using UnityEngine.AI;

namespace Tanks.Game
{
    public class EnemyController : MonoBehaviour
    {
        public const string DamageAreaTag = "DamageArea";

        [SerializeField]
        private NavMeshAgent navMeshAgent;

        private void Update()
        {
            if (WorldEntitiesManager.Instance.Player != null)
            {
                navMeshAgent.destination = WorldEntitiesManager.Instance.Player.transform.position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == DamageAreaTag)
            {
                navMeshAgent.stoppingDistance = navMeshAgent.remainingDistance;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == DamageAreaTag)
            {
                navMeshAgent.stoppingDistance = 0;
            }
        }
    }
}
