using UnityEngine;

namespace Tanks.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public delegate void DeathDelegate();

        public event DeathDelegate Death;

        public bool Dead
        { get; private set; }

        [SerializeField]
        private PlayerMovement movement;

        private void OnEnable()
        {
            World.Player.Register(this);
        }

        private void OnDisable()
        {
            World.Player.Unregister(this);
        }

        private void OnDestroy()
        {
            Death = null;
        }

        public void SetPosition(Vector3 position)
        {
            if (movement != null)
            {
                movement.SetPositionImmediately(position);
            }
            else
            {
                transform.position = position;
            }
        }

        public void OnDeath()
        {
            Dead = true;
            Death?.Invoke();
        }
    }
}
