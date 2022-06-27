using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemySpawner : MonoBehaviour
    {
        public int Id => GetInstanceID();

        [SerializeField]
        private EnemySpawnerConfiguration config;
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
            var index = Random.Range(0, config.Presets.Length);
            var preset = config.Presets[index];

            var enemy = PrefabPool.Instantiate(preset.Prefab);
            enemy.SetPosition(pivot.transform.position);
        }
    }
}
