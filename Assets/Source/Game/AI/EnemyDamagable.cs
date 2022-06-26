using Tanks.Game.Damage;
using UnityEngine;
using UnityEngine.Events;

namespace Tanks.Game.AI
{
    public class EnemyDamagable : MonoBehaviour, IDamagableGameObject
    {
        public UnityEvent Death;
        public UnityEvent<DamageInfo, float> HealthChanged;

        public int Id => GetInstanceID();
        public float HealthCapacity => healthCapacity;
        public float Health => health;
        public bool IsDead => dead;
        public GameObject GameObject => this == null ? null : gameObject;

        [SerializeField]
        private float health;
        [SerializeField]
        private float healthCapacity;

        private bool dead;

        private void OnEnable()
        {
            World.Damage.Register(this);
            OnHealthChanged(0, default);
        }

        private void OnDisable()
        {
            World.Damage.Unregister(this);
        }

        public void Damage(float value, DamageInfo info)
        {
            if (dead)
            {
                return;
            }

            var delta = Mathf.Clamp(value, 0, health);
            health -= delta;

            OnHealthChanged(-delta, info);
        }

        private void OnHealthChanged(float delta, DamageInfo info)
        {
            if (!dead && health <= 0)
            {
                dead = true;
                Death?.Invoke();
            }
            else if (dead && health > 0)
            {
                dead = false;
            }

            if (delta != 0)
            {
                HealthChanged?.Invoke(info, delta);
            }
        }
    }
}
