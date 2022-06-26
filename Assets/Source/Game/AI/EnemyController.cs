using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemyController : MonoBehaviour
    {
        public int Id => GetInstanceID();

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
            Destroy(gameObject);
        }
    }
}
