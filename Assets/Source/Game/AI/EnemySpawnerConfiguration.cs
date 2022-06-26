using System;
using UnityEngine;

namespace Tanks.Game.AI
{
    [CreateAssetMenu(fileName = "Enemy Spawner Configuration", menuName = "Tanks/Enemies/Spawner Configuration")]
    public class EnemySpawnerConfiguration : ScriptableObject
    {
        [Serializable]
        public struct EnemyPreset
        {
            public EnemyController Prefab;
        }

        public EnemyPreset[] Presets;
    }
}
