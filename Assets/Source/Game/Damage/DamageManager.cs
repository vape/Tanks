﻿using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Game.Damage
{
    public class DamageManager
    {
        public delegate void EntityRegistrationDelegate(IDamagableEntity entity);

        public event EntityRegistrationDelegate EntityRegistered;
        public event EntityRegistrationDelegate EntityUnregistered;

        public IReadOnlyCollection<IDamagableEntity> Entities => damagableEntities.Values;

        private Dictionary<int, IDamagableEntity> damagableEntities = new Dictionary<int, IDamagableEntity>();

        public bool TryFindEntity(int id, out IDamagableEntity entity)
        {
            return damagableEntities.TryGetValue(id, out entity);
        }

        public void Register(GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                var damagable = component as IDamagableEntity;
                if (damagable != null)
                {
                    Register(damagable);
                }
            }
        }

        public void Register(IDamagableEntity entity)
        {
            var id = entity.Id;
            if (damagableEntities.ContainsKey(id))
            {
                throw new System.Exception("Entity already registered!");
            }

            damagableEntities.Add(id, entity);
            EntityRegistered?.Invoke(entity);
        }

        public void Unregsiter(GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponents<Component>())
            {
                var damagable = component as IDamagableEntity;
                if (damagable != null)
                {
                    Unregister(damagable);
                }
            }
        }

        public void Unregister(IDamagableEntity entity)
        {
            damagableEntities.Remove(entity.Id);
            EntityUnregistered?.Invoke(entity);
        }

        public void Damage(GameObject gameObject, float damage, DamageInfo info = default)
        {
            foreach (var component in gameObject.GetComponentsInParent<Component>())
            {
                var damagable = component as IDamagableEntity;
                if (damagable != null)
                {
                    TryDamage(damagable, damage, info);
                }
            }
        }

        public bool TryDamage(int target, float damage, DamageInfo info = default)
        {
            if (!damagableEntities.TryGetValue(target, out var entity))
            {
                return false;
            }

            return TryDamage(entity, damage, info);
        }

        public bool TryDamage(IDamagableEntity entity, float damage, DamageInfo info = default)
        {
            if (entity.IsDead)
            {
                return false;
            }

//#if DEBUG
//            var sourceName = info.Source == null ? "something" : info.Source.name;
//            Debug.Log($"{sourceName} is damaging {GenerateEntityDescription(entity)} for {damage} hit points");
//#endif

            entity.Damage(damage, info);
            return true;
        }

        private string GenerateEntityDescription(IDamagableEntity entity)
        {
            var goEntity = entity as IDamagableGameObject;
            if (goEntity != null && goEntity.GameObject != null)
            {
                return $"{goEntity.GameObject.name}:{entity.Id}";
            }
            else
            {
                return $"entity:{entity.Id}";
            }
        }
    }
}
