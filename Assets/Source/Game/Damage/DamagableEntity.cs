using UnityEngine;
using UnityEngine.Events;

namespace Tanks.Game.Damage
{
    public class DamagableEntity : MonoBehaviour
    {
        public bool IsDead => dead;

        public UnityEvent Death;

        [SerializeField]
        private float health;

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

        public void Damage(float value)
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
