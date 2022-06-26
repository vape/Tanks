using System;
using UnityEngine;

namespace Tanks.Game
{
    public struct ProjectileData
    {
        public Vector3 Direction;
        public float TimeToLive;
        public float Velocity;
        public Projectile Instance;
    }

    public class ProjectileManager
    {
        private ProjectileData[] projectiles;
        private int slot;

        public ProjectileManager()
        {
            projectiles = new ProjectileData[128];
            slot = 0;
        }

        private void Resize()
        {
            var size = projectiles.Length;
            var nextSize = size * 2;

            Array.Resize(ref projectiles, nextSize);
        }

        public void Create(Projectile prefab, Vector3 origin, Vector3 direction, float velocity, float timeToLive)
        {
            var instance = PrefabPool.Instantiate(prefab);
            instance.transform.position = origin;
            instance.transform.rotation = Quaternion.LookRotation(direction, new Vector3(-direction.y, direction.x));

            if (projectiles.Length == slot)
            {
                Resize();
            }

            projectiles[slot++] = new ProjectileData() { Direction = direction, Instance = instance, TimeToLive = timeToLive, Velocity = velocity };
        }

        // TODO: resize array down when its filled for less than 1/4 of its size
        private void Destroy(int index)
        {
            if (projectiles[index].Instance != null)
            {
                PrefabPool.Destroy(projectiles[index].Instance);
            }

            for (int i = index; i < slot; ++i)
            {
                projectiles[i] = projectiles[i + 1];
            }

            slot--;
            projectiles[slot] = default;
        }

        // TODO: destroy by segments, not one-by-one
        public void Update()
        {
            for (int i = slot - 1; i >= 0; --i)
            {
                if (projectiles[i].Instance == null)
                {
                    Destroy(i);
                    continue;
                }

                projectiles[i].Instance.transform.position += projectiles[i].Direction * projectiles[i].Velocity * Time.deltaTime;
                projectiles[i].TimeToLive -= Time.deltaTime;

                if (projectiles[i].TimeToLive <= 0 || projectiles[i].Instance.IsDead)
                {
                    Destroy(i);
                }
            }
        }
    }
}
