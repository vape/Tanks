using Tanks.Game.AI;
using Tanks.Game.Damage;
using Tanks.Game.Player;
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

        public static ProjectileManager Projectiles
        { get; private set; } = new ProjectileManager();
        public static DamageManager Damage
        { get; private set; } = new DamageManager();
        public static EnemySystem Enemies
        { get; private set; } = new EnemySystem();
        public static PlayerSystem Player
        { get; private set; } = new PlayerSystem();

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
