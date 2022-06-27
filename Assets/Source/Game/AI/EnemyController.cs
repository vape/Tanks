using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.Game.AI
{
    public class EnemyController : MonoBehaviour
    {
        public int Id => GetInstanceID();

        [SerializeField]
        private float despawnDelay;
        [SerializeField]
        private NavMeshAgent navMesh;

        private void OnEnable()
        {
            World.Enemies.Register(this);
        }

        private void OnDisable()
        {
            World.Enemies.Unregister(this);
        }

        public void SetPosition(Vector3 position)
        {
            if (navMesh == null)
            {
                transform.position = position;
            }
            else
            {
                navMesh.enabled = false;
                transform.position = position;
                navMesh.enabled = true;
            }
        }

        public void OnDeath()
        {
            StartCoroutine(DespawnRoutine());
        }

        private IEnumerator DespawnRoutine()
        {
            yield return new WaitForSeconds(despawnDelay);
            Destroy(gameObject);
        }
    }
}
