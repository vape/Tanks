using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Game.Arena
{
    public class ArenaController : MonoBehaviour
    {
        public delegate void ArenaLoadingCallbackDelegate(ArenaController arena);

        public static ArenaController Instance
        { get; private set; }

        private static HashSet<ArenaLoadingCallbackDelegate> loadingCallbacks = new HashSet<ArenaLoadingCallbackDelegate>();
        private static HashSet<ArenaLoadingCallbackDelegate> unloadingCallbacks = new HashSet<ArenaLoadingCallbackDelegate>();

        public static void RegisterLoadingCallback(ArenaLoadingCallbackDelegate callback)
        {
            if (Instance != null)
            {
                callback?.Invoke(Instance);
                return;
            }

            loadingCallbacks.Add(callback);
        }

        public static void RegisterUnloadingCallback(ArenaLoadingCallbackDelegate callback)
        {
            unloadingCallbacks.Add(callback);
        }

        public static void UnregisterLoadingCallback(ArenaLoadingCallbackDelegate callback)
        {
            loadingCallbacks.Remove(callback);
        }

        public static void UnregisterUnloadingCallback(ArenaLoadingCallbackDelegate callback)
        {
            unloadingCallbacks.Remove(callback);
        }

        public ArenaEnemySpawner EnemySpawner => enemySpawner;
        public ArenaPlayerSpawner PlayerSpawner => playerSpawner;

        [SerializeField]
        private ArenaEnemySpawner enemySpawner;
        [SerializeField]
        private ArenaPlayerSpawner playerSpawner;

        private void Awake()
        {
            if (Instance != null)
            {
                throw new Exception("Only one arena controller is allowed simultaneously!");
            }

            Instance = this;

            var callbacks = new HashSet<ArenaLoadingCallbackDelegate>(loadingCallbacks);
            loadingCallbacks.Clear();

            foreach (var callback in callbacks)
            {
                callback?.Invoke(Instance);
            }
        }

        private void OnDestroy()
        {
            Instance = null;

            var callbacks = new HashSet<ArenaLoadingCallbackDelegate>(unloadingCallbacks);
            unloadingCallbacks.Clear();

            foreach (var callback in callbacks)
            {
                callback?.Invoke(Instance);
            }
        }
    }
}
