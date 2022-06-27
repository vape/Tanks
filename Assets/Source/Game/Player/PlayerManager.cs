using System;
using System.Collections.Generic;
using Tanks.Game.Arena;
using UnityEngine;

namespace Tanks.Game.Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance
        { get; private set; }
        public static PlayerController PlayerInstance
        { get { return Instance == null ? null : Instance.Player; } }

        public PlayerController Player
        { get; private set; }

        [SerializeField]
        private PlayerManagerConfiguration config;
        [SerializeField]
        private PlayerCameraFollower cameraFollower;

        private ArenaController arena;
        private PlayerManagerConfiguration.PlayerPreset lastPreset;

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Only one player manager is allowed simultaneously!");
            }

            Instance = this;

            ArenaController.RegisterLoadingCallback(ArenaLoadedHandler);
        }

        private void OnDestroy()
        {
            ArenaController.UnregisterLoadingCallback(ArenaLoadedHandler);
            ArenaController.UnregisterUnloadingCallback(ArenaLoadedHandler);

            Instance = null;
        }

        private void ArenaLoadedHandler(ArenaController arena)
        {
            this.arena = arena;

            ArenaController.RegisterUnloadingCallback(ArenaUnloadedHandler);
        }

        private void ArenaUnloadedHandler(ArenaController arena)
        {
            this.arena = null;

            ArenaController.RegisterLoadingCallback(ArenaLoadedHandler);
        }

        public bool CanSpawn()
        {
            return arena != null;
        }

        public void Spawn(string presetName)
        {
            if (Player != null)
            {
                throw new Exception("Player already exist!");
            }

            if (!config.TryFindPreset(presetName, out var preset))
            {
                throw new Exception($"Failed to find preset {presetName}");
            }

            lastPreset = preset;
            Player = arena.Spawner.Spawn(preset.Prefab);
            cameraFollower.Player = Player.gameObject;
        }

        public void Despawn()
        {
            if (Player == null)
            {
                return;
            }

            cameraFollower.Player = null;
            Destroy(Player.gameObject);
            Player = null;
        }

        public void Respawn()
        {
            Respawn((Player == null ? config.Presets[0] : lastPreset).Name);
        }

        public void Respawn(string presetName)
        {
            if (Player != null)
            {
                Despawn();
            }

            Spawn(presetName);
        }
    }
}
