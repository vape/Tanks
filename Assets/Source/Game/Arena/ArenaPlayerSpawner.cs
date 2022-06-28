using System;
using Tanks.Game.Player;
using UnityEngine;

namespace Tanks.Game.Arena
{
    public class ArenaPlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private PlayerController prefab;
        [SerializeField]
        private Transform origin;

        public void Spawn()
        {
            if (World.Player.Controller != null)
            {
                throw new Exception("Player already exist!");
            }

            var player = GameObject.Instantiate(prefab);
            player.SetPosition(origin.transform.position);
        }

        public void Despawn()
        {
            if (World.Player.Controller == null)
            {
                return;
            }

            GameObject.Destroy(World.Player.Controller.gameObject);
        }

        public void Respawn()
        {
            if (World.Player.Controller != null)
            {
                Despawn();
            }

            Spawn();
        }
    }
}
