using System.Threading.Tasks;
using Tanks.Game.Mode;
using Tanks.Game.Mode.Modes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tanks.Game.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameModeManager gameModeManager;

        private bool levelLoaded;
        private LevelManifest activeManifest;
         
        private void Update()
        {
            if (Keyboard.current[Key.Escape].wasPressedThisFrame)
            {
                GameNavigation.LoadMenu();
            }
        }

        public async Task Load(LevelManifest manifest)
        {
            if (levelLoaded)
            {
                await Unload();
            }

            activeManifest = manifest;
            await LoadArena(manifest.ArenaName);
            gameModeManager.Load(manifest.GameMode);
            levelLoaded = true;
        }

        public async Task Unload()
        {
            if (!levelLoaded)
            {
                return;
            }

            await UnloadArena(activeManifest.ArenaName);
            gameModeManager.Unload();
            activeManifest = default;
            levelLoaded = false;
        }

        private async Task UnloadArena(string name)
        {
            SceneManager.SetActiveScene(gameObject.scene);

            var completionSource = new TaskCompletionSource<bool>();
            SceneManager.UnloadSceneAsync(name).completed += (_) => completionSource.SetResult(true);
            await completionSource.Task;
        }

        private async Task LoadArena(string name)
        {
            var completionSource = new TaskCompletionSource<bool>();
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).completed += (_) => completionSource.SetResult(true);
            await completionSource.Task;

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        }

        private LevelManifest GetManifest(int arena)
        {
            return new LevelManifest()
            {
                ArenaName = arena == 0 ? "Arena_01_Test_Grass" : "Arena_02_Test_Desert", 
                GameMode = ScriptableObject.CreateInstance<EndlessGameModeConfiguration>()
            };
        }
    }
}
