using System.Collections.Generic;

namespace Tanks.Game.AI
{
    public class EnemySystem
    {
        public delegate void EnemyRegistrationDelegate(EnemyController enemy);

        public event EnemyRegistrationDelegate EnemyRegistered;
        public event EnemyRegistrationDelegate EnemyUnregistered;

        public IReadOnlyCollection<EnemyController> Enemies => enemies.Values;

        private Dictionary<int, EnemyController> enemies = new Dictionary<int, EnemyController>();

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
    }
}
