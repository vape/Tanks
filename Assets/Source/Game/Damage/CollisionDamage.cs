using UnityEngine;
using UnityEngine.Events;

namespace Tanks.Game.Damage
{
    [RequireComponent(typeof(Collider))]
    public class CollisionDamage : MonoBehaviour
    {
        public UnityEvent<Collider> Triggered;

        public bool IsTriggered => triggered;

        [SerializeField]
        private float damage;

        private bool triggered;

        private void OnTriggerEnter(Collider other)
        {
            World.Damage.Damage(other.gameObject, damage, new DamageInfo() { Source = gameObject });

            triggered = true;
            Triggered?.Invoke(other);
        }
    }
}
