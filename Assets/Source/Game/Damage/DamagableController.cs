using UnityEngine;
using UnityEngine.Events;

namespace Tanks.Game.Damage
{
    public class DamagableController : MonoBehaviour, IDamagableGameObject
    {
        public UnityEvent Death;

        public int Id => GetInstanceID();
        public float Protection => protection;
        public float HealthCapacity => healthCapacity;
        public GameObject GameObject => this == null ? null : gameObject;

        public float Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public bool IsDead
        {
            get
            {
                return dead;
            }
            set
            {
                dead = value;
            }
        }

        [SerializeField]
        private float health;
        [SerializeField]
        private float healthCapacity;
        [SerializeField]
        [Range(0, 1)]
        private float protection;

        private bool dead;

        private void OnEnable()
        {
            World.Damage.Register(this);
        }

        private void OnDisable()
        {
            World.Damage.Unregister(this);
        }

        public void OnDamage(float value, DamageInfo info)
        { }

        public void OnDeath(DamageInfo info)
        {
            Death?.Invoke();
        }
    }
}
