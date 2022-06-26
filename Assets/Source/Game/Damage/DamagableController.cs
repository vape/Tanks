using UnityEngine;
using UnityEngine.Events;

namespace Tanks.Game.Damage
{
    public class DamagableController : MonoBehaviour, IDamagableGameObject
    {
        public UnityEvent Death;

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
            OnHealthChanged();
        }

        private void OnDisable()
        {
            World.Damage.Unregister(this);
        }

        public void Damage(float value, DamageInfo info)
        {
            health = Mathf.Max(0, health - value);
            OnHealthChanged();
        }

        private void OnHealthChanged()
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
        }
    }
}
