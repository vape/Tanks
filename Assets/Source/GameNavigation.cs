using System;
using System.Threading.Tasks;
using Tanks.Game.Level;
using Tanks.UI.Transition;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public static class GameNavigation
    {
        public const string MainGameSceneName = "Main";
        public const string MenuSceneName = "Menu";

        private static bool Loading;

        public static void LoadGame(LevelManifest manifest)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            LoadGameAsync(manifest);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public static async Task LoadGameAsync(LevelManifest manifest)
        {
            if (Loading)
            {
                return;
            }

            Loading = true;

            var transition = await TransitionService.CreateTransition();

            await transition.ShowAsync();

            if (SceneManager.GetSceneByName(MenuSceneName).isLoaded)
            {
                await UnloadSceneAsync(MenuSceneName);
            }

            await LoadSceneAsync(MainGameSceneName);

            var scene = SceneManager.GetSceneByName(MainGameSceneName);
            while (!scene.isLoaded)
            {
                await Task.Yield();
            }

            LevelManager levelManager = null;
            foreach (var gameObject in scene.GetRootGameObjects())
            {
                levelManager = gameObject.GetComponent<LevelManager>();

                if (levelManager != null)
                {
                    break;
                }
            }

            if (levelManager == null)
            {
                throw new Exception("Failed to find level manager");
            }

            await levelManager.LoadAsync(manifest);

            await transition.HideAsync();
            await transition.DestroyAsync();

            Loading = false;
        }

        public static void LoadMenu()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            LoadMenuAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public static async Task LoadMenuAsync()
        {
            if (Loading)
            {
                return;
            }

            Loading = true;

            var transition = await TransitionService.CreateTransition();

            transition.ToggleTransitionCamera(false);
            await transition.ShowAsync();
            transition.ToggleTransitionCamera(true);
            
            if (SceneManager.GetSceneByName(MainGameSceneName).isLoaded)
            {
                var gameScene = SceneManager.GetSceneByName(MainGameSceneName);

                LevelManager levelManager = null;
                foreach (var gameObject in gameScene.GetRootGameObjects())
                {
                    levelManager = gameObject.GetComponentInChildren<LevelManager>();

                    if (levelManager != null)
                    {
                        break;
                    }
                }

                if (levelManager == null)
                {
                    throw new Exception("Failed to find level manager");
                }

                await levelManager.Unload();
                await UnloadSceneAsync(MainGameSceneName);
            }

            await LoadSceneAsync(MenuSceneName);

            await transition.DestroyAsync();

            Loading = false;
        }

        private static async Task LoadSceneAsync(string name)
        {
            var sceneLoadingSource = new TaskCompletionSource<bool>();
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).completed += (_) => sceneLoadingSource.SetResult(true);
            await sceneLoadingSource.Task;
        }

        private static async Task UnloadSceneAsync(string name)
        {
            var sceneUnloadingSource = new TaskCompletionSource<bool>();
            SceneManager.UnloadSceneAsync(name).completed += (_) => sceneUnloadingSource.SetResult(true);
            await sceneUnloadingSource.Task;
        }
    }
}
