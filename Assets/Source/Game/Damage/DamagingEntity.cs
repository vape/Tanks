using UnityEngine;
using UnityEngine.Events;

namespace Tanks.Game.Damage
{
    public class DamagingEntity : MonoBehaviour
    {
        public UnityEvent<Collider> Collided;

        public bool IsCollided => collided;

        [SerializeField]
        private float damage;

        private bool collided;

        private void OnTriggerEnter(Collider other)
        {
            World.Damage.TryDamage(other.gameObject, damage);
            collided = true;
            Collided?.Invoke(other);
        }
    }
}
