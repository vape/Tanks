using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Game.Damage
{
    public class DamageManager
    {
        private Dictionary<int, DamagableEntity> damagableEntities = new Dictionary<int, DamagableEntity>();

        public void Register(DamagableEntity entity)
        {
            var id = entity.gameObject.GetInstanceID();
            if (damagableEntities.ContainsKey(id))
            {
                throw new System.Exception("Entity already registered!");
            }

            damagableEntities.Add(id, entity);
        }

        public void Unregister(DamagableEntity entity)
        {
            damagableEntities.Remove(entity.gameObject.GetInstanceID());
        }

        public bool TryDamage(GameObject target, float damage)
        {
            if (!damagableEntities.TryGetValue(target.GetInstanceID(), out var entity))
            {
                return false;
            }

            if (entity.IsDead)
            {
                return false;
            }

            entity.Damage(damage);
            return true;
        }
    }
}
