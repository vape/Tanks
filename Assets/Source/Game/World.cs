using Tanks.Game.Damage;
using UnityEngine;

namespace Tanks.Game
{
    public static class World
    {
        private class UnityEventsBridge : MonoBehaviour
        {
            private void Update()
            {
                World.Update();
            }
        }

        public static EntitiesManager Entities
        { get; private set; } = new EntitiesManager();
        public static ProjectileManager Projectiles
        { get; private set; } = new ProjectileManager();
        public static DamageManager Damage
        { get; private set; } = new DamageManager();

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize()
        {
            var bridge = new GameObject("Unity Events Bridge");
            bridge.AddComponent<UnityEventsBridge>();
            GameObject.DontDestroyOnLoad(bridge);
        }

        private static void Update()
        {
            Projectiles.Update();
        }
    }
}
