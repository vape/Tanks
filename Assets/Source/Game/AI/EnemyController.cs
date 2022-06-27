using System.Collections;
using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemyController : MonoBehaviour
    {
        public int Id => GetInstanceID();

        [SerializeField]
        private float despawnDelay;

        private void OnEnable()
        {
            World.Enemies.Register(this);
        }

        private void OnDisable()
        {
            World.Enemies.Unregister(this);
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
