using System.Collections.Generic;
using Tanks.Game.AI;
using UnityEngine;

namespace Tanks.Game.Arena
{
    public class ArenaEnemySpawner : MonoBehaviour
    {
        public IReadOnlyList<ArenaEnemySpawnerSpot> Spots => spots;

        [SerializeField]
        private ArenaEnemySpawnerSpot[] spots;
        [SerializeField]
        private EnemiesConfig config;

        public EnemyController Spawn(string name, ArenaEnemySpawnerSpot spot)
        {
            if (config.TryFind(name, out var preset))
            {
                var controller = GameObject.Instantiate(preset.Prefab);
                controller.SetPosition(spot.Origin.position);

                return controller;
            }

            return null;
        }

        public void Despawn(EnemyController controller)
        {
            GameObject.Destroy(controller.gameObject);
        }
    }
}
