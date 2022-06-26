using UnityEngine;

namespace Tanks.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        public delegate void DeathDelegate();

        public event DeathDelegate Death;

        public bool Dead
        { get; private set; }

        public void OnDeath()
        {
            Dead = true;
            Death?.Invoke();
        }

        private void OnDestroy()
        {
            Death = null;
        }
    }
}
