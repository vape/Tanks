using UnityEngine;
using UnityEngine.AI;

namespace Tanks.Game.AI
{
    public class EnemyPlayerFollower : EnemyMovement
    {
        public const string DamageAreaTag = "DamageArea";
        public override Vector3 Velocity => navMeshAgent.velocity;

        [SerializeField]
        private NavMeshAgent navMeshAgent;

        private void Update()
        {
            if (World.Player.Controller != null)
            {
                navMeshAgent.destination = World.Player.Controller.transform.position;
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

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        public override void SetPositionImmediately(Vector3 position)
        {
            navMeshAgent.enabled = false;
            transform.position = position;
            navMeshAgent.enabled = true;
        }
    }
}
