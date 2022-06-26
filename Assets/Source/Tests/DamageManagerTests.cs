using NUnit.Framework;
using System;
using Tanks.Game.Damage;

namespace Tanks.Tests
{
    public class DamageManagerTests
    {
        public class DamagableEntity : IDamagableEntity
        {
            private static int counter;

            public int Id => id;
            public float Health => health;
            public float HealthCapacity => healthCapacity;
            public bool IsDead => dead;

            private int id;
            private float health;
            private float healthCapacity;
            private bool dead;

            public DamagableEntity(float health)
            {
                this.id = counter++;
                this.health = health;
                this.healthCapacity = health;

                OnHealthChanged();
            }

            public void Damage(float value, DamageInfo info)
            {
                health -= value;
            }

            private void OnHealthChanged()
            {
                dead = health == 0;
            }
        }

        [Test]
        public void DealDamage()
        {
            var damageManager = new DamageManager();
            var entity = new DamagableEntity(100);
            damageManager.Register(entity);

            Assert.True(damageManager.TryDamage(entity, 10), "Failed to damage entity");
            Assert.AreEqual(90, entity.Health, "Unexpected health value", entity.Health);
        }

        [Test]
        public void EntityRegistration()
        {
            var damageManager = new DamageManager();
            var entity = new DamagableEntity(100);
            damageManager.Register(entity);

            Assert.True(damageManager.TryFindEntity(entity.Id, out var foudEntity));
            Assert.AreEqual(entity.Id, foudEntity.Id, "Wrong entity found");
            Assert.Throws<Exception>(() => damageManager.Register(entity), "Same entity registered twice");

            damageManager.Unregister(entity);
            Assert.False(damageManager.TryFindEntity(entity.Id, out var foundEntity2), "Found unregistered entity");
        }
    }
}
