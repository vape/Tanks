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
            if (World.Entities.Player != null)
            {
                navMeshAgent.destination = World.Entities.Player.transform.position;
            }
        }

        public void OnDeath()
        {
            Destroy(gameObject);
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
