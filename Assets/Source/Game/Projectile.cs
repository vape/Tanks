using UnityEngine;

namespace Tanks.Game
{
    public class Projectile : MonoBehaviour, IPoolSpawnedHandler
    {
        public bool IsDead
        { get; private set; }    

        public void MarkDead()
        {
            IsDead = true;
        }

        public void OnSpawned()
        {
            IsDead = false;
        }
    }
}
