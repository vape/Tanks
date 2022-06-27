using NUnit.Framework;
using System;
using Tanks.Game.Damage;
using UnityEngine;

namespace Tanks.Tests
{
    public class DamageManagerTests
    {
        public class DamagableEntity : IDamagableEntity
        {
            private static int counter;

            public int Id
            { get; private set; }
            public float Protection
            { get; private set; }
            public float HealthCapacity
            { get; private set; }

            public float Health
            { get; set; }
            public bool IsDead
            { get; set; }

            public DamagableEntity(float health, float capacity, float protection)
            {
                Id = counter++;
                Health = health;
                HealthCapacity = capacity;
                Protection = protection;
            }

            public void OnDamage(float value, DamageInfo info)
            { }

            public void OnDeath(DamageInfo info)
            { }
        }

        [Test]
        public void DealDamage()
        {
            var damageManager = new DamageManager();
            var entity = new DamagableEntity(100, 100, 0);
            damageManager.Register(entity);

            Assert.True(damageManager.TryDamage(entity, 10), "Failed to damage entity");
            Assert.AreEqual(90, entity.Health, "Unexpected health value");

            var entityProtected = new DamagableEntity(100, 100, 0.75f);
            damageManager.Register(entityProtected);

            Assert.True(damageManager.TryDamage(entityProtected, 100), "Failed to damage protected entity");
            Assert.That(entityProtected.Health, Is.EqualTo(75f).Within(0.001f), "Unexpected health value");
            Assert.True(damageManager.TryDamage(entityProtected, 300), "Failed to damage protected entity second time");
            Assert.That(entityProtected.Health, Is.EqualTo(0f), "Unexpected health value");
            Assert.True(entityProtected.IsDead, "Entity is not dead after deadly damage");
            
            var entityDead = new DamagableEntity(0, 100, 0);
            damageManager.Register(entityDead);

            Assert.True(entityDead.IsDead, "Dead entity is not dead");
        }

        [Test]
        public void EntityRegistration()
        {
            var damageManager = new DamageManager();
            var entity = new DamagableEntity(100, 100, 0);
            damageManager.Register(entity);

            Assert.True(damageManager.TryFindEntity(entity.Id, out var foudEntity));
            Assert.AreEqual(entity.Id, foudEntity.Id, "Wrong entity found");
            Assert.Throws<Exception>(() => damageManager.Register(entity), "Same entity registered twice");

            damageManager.Unregister(entity);
            Assert.False(damageManager.TryFindEntity(entity.Id, out var foundEntity2), "Found unregistered entity");
        }
    }
}
