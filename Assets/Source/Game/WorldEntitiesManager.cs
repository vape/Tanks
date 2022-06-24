using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks.Game
{
    public class WorldEntitiesManager : MonoBehaviour
    {
        public static WorldEntitiesManager Instance
        {
            get
            {
                if (!created)
                {
                    var gameObject = new GameObject("Entities Manager");
                    instance = gameObject.AddComponent<WorldEntitiesManager>();
                    created = true;
                }

                return instance;
            }
        }

        public delegate void PlayerRegisteredDelegate(PlayerController controller);
        public delegate void PlayerUnregisteredDelegate(PlayerController controller);

        public event PlayerRegisteredDelegate PlayerRegistered;
        public event PlayerUnregisteredDelegate PlayerUnregistered;

        private static bool created;
        private static WorldEntitiesManager instance;

        public PlayerController Player
        { get { return player; } }

        private PlayerController player;

        private void OnEnable()
        {
            SceneManager.sceneUnloaded += SceneUnloadedHandler;
        }

        private void OnDisable()
        {
            SceneManager.sceneUnloaded -= SceneUnloadedHandler;
        }

        private void SceneUnloadedHandler(Scene scene)
        {
            if (gameObject.scene == scene && created)
            {
                Destroy(instance);
                created = false;
            }
        }

        public void RegisterPlayer(PlayerController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (player != null)
            {
                throw new Exception("Player already registered!");
            }

            player = controller;
            PlayerRegistered?.Invoke(player);
        }

        public void UnregisterPlayer(PlayerController controller)
        {
            if (player != controller)
            {
                player = null;
                PlayerUnregistered?.Invoke(controller);
            }
        }
    }
}
