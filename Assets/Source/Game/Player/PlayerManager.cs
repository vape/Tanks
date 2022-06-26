using System;
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
        private PlayerSpawner spawner;
        [SerializeField]
        private PlayerCameraFollower cameraFollower;

        private PlayerManagerConfiguration.PlayerPreset lastPreset;

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Only one player manager is allowed simultaneously!");
            }

            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
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
            Player = spawner.Spawn(preset.Prefab);
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
