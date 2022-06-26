using System.Collections.Generic;

namespace Tanks.Game.AI
{
    public class EnemyManager
    {
        public delegate void EnemyRegistrationDelegate(EnemyController enemy);
        public delegate void SpawnerRegistrationDelegate(EnemySpawner spawner);

        public event EnemyRegistrationDelegate EnemyRegistered;
        public event EnemyRegistrationDelegate EnemyUnregistered;

        public event SpawnerRegistrationDelegate SpawnerRegistered;
        public event SpawnerRegistrationDelegate SpawnerUnregistered;

        public IReadOnlyList<EnemySpawner> Spawners => spawners;
        public IReadOnlyCollection<EnemyController> Enemies => enemies.Values;

        private Dictionary<int, EnemyController> enemies = new Dictionary<int, EnemyController>();
        private List<EnemySpawner> spawners = new List<EnemySpawner>();

        public bool TryFindEnemy(int id, out EnemyController enemy)
        {
            return enemies.TryGetValue(id, out enemy);
        }

        public bool TryFindSpawner(int id, out EnemySpawner spawner)
        {
            for (int i = 0; i < spawners.Count; ++i)
            {
                if (spawners[i].Id == id)
                {
                    spawner = spawners[i];
                    return true;
                }
            }

            spawner = default;
            return false;
        }

        public void Register(EnemySpawner spawner)
        {
            var id = spawner.Id;

            if (TryFindSpawner(spawner.Id, out _))
            {
                throw new System.Exception("Spawner already registered!");
            }

            spawners.Add(spawner);
            SpawnerRegistered?.Invoke(spawner);
        }

        public void Register(EnemyController enemy)
        {
            var id = enemy.Id;

            if (enemies.ContainsKey(id))
            {
                throw new System.Exception("Enemy already registered!");
            }

            enemies.Add(id, enemy);
            EnemyRegistered?.Invoke(enemy);
        }

        public void Unregister(EnemyController enemy)
        {
            if (enemies.Remove(enemy.Id))
            {
                EnemyUnregistered?.Invoke(enemy);
            }
        }

        public void Unregister(EnemySpawner spawner)
        {
            if (spawners.Remove(spawner))
            {
                SpawnerUnregistered?.Invoke(spawner);
            }
        }
    }
}
