using System.Threading.Tasks;
using Tanks.Assets.Source.Game;
using Tanks.Game.Mode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tanks.Game.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameModeManager gameModeManager;
        [SerializeField]
        private GameSettings gameSettings;

        private bool levelLoaded;
        private LevelManifest activeManifest;

        private void Awake()
        {
#if UNITY_EDITOR
            // allow to start game by opening main and arena scenes in editor
            for (int i = 0; i < gameSettings.Arenas.Length; ++i)
            {
                var scene = SceneManager.GetSceneByName(gameSettings.Arenas[i].SceneName);
                if (scene.isLoaded)
                {
                    gameModeManager.Load(gameSettings.Modes[0]);
                    break;
                }
            }
#endif
        }

        private void Update()
        {
            if (Keyboard.current[Key.Escape].wasPressedThisFrame)
            {
                GameNavigation.LoadMenu();
            }
        }

        public async Task LoadAsync(LevelManifest manifest)
        {
            if (levelLoaded)
            {
                await Unload();
            }

            activeManifest = manifest;
            await LoadArenaAsync(manifest.Arena.SceneName);
            gameModeManager.Load(manifest.GameMode);
            levelLoaded = true;
        }

        public async Task Unload()
        {
            if (!levelLoaded)
            {
                return;
            }

            await UnloadArenaAsync(activeManifest.Arena.SceneName);
            gameModeManager.Unload();
            activeManifest = default;
            levelLoaded = false;
        }

        private async Task UnloadArenaAsync(string name)
        {
            SceneManager.SetActiveScene(gameObject.scene);

            var completionSource = new TaskCompletionSource<bool>();
            SceneManager.UnloadSceneAsync(name).completed += (_) => completionSource.SetResult(true);
            await completionSource.Task;
        }

        private async Task LoadArenaAsync(string name)
        {
            var completionSource = new TaskCompletionSource<bool>();
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).completed += (_) => completionSource.SetResult(true);
            await completionSource.Task;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        }
    }
}
