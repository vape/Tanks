using System;
using System.Collections;
using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemySpawner : MonoBehaviour
    {
        public int Id => GetInstanceID();

        [Serializable]
        public struct EnemyPreset
        {
            public EnemyController Controller;
        }

        [SerializeField]
        private EnemyPreset[] presets;
        [SerializeField]
        private Transform pivot;

        private void OnEnable()
        {
            World.Enemies.Register(this);
        }

        private void OnDisable()
        {
            World.Enemies.Unregister(this);
        }

        public void Spawn()
        {
            var index = UnityEngine.Random.Range(0, presets.Length);
            var preset = presets[index];

            var enemy = PrefabPool.Instantiate(preset.Controller);
            enemy.transform.position = pivot.transform.position;
        }
    }
}
