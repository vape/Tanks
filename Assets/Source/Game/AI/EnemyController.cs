using System.Collections;
using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemyController : MonoBehaviour
    {
        public int Id => GetInstanceID();

        public delegate void DeathDelegate();

        public event DeathDelegate Death;

        public bool Dead
        { get; private set; }

        [SerializeField]
        private float deathDelay;
        [SerializeField]
        private EnemyMovement movement;

        private bool pendingDead;

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
            if (movement == null)
            {
                transform.position = position;
            }
            else
            {
                movement.SetPositionImmediately(position);
            }
        }

        public void OnDeath()
        {
            if (pendingDead || Dead)
            {
                return;
            }

            pendingDead = true;
            StartCoroutine(DespawnRoutine());
        }

        private IEnumerator DespawnRoutine()
        {
            yield return new WaitForSeconds(deathDelay);

            Dead = true;
            Death?.Invoke();
            pendingDead = false;
        }
    }
}
