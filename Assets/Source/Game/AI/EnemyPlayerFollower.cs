using UnityEngine;
using UnityEngine.AI;

namespace Tanks.Game.AI
{
    public class EnemyPlayerFollower : MonoBehaviour
    {
        public const string DamageAreaTag = "DamageArea";

        [SerializeField]
        private NavMeshAgent navMeshAgent;

        private void Update()
        {
            if (World.Entities.Player != null)
            {
                navMeshAgent.destination = World.Entities.Player.transform.position;
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
