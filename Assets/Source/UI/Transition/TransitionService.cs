using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Tanks.UI.Transition
{
    public static class TransitionService
    {
        private const string TransitionSceneName = "Transition";

        public static async Task<TransitionHandle> CreateTransition()
        {
            var sceneLoadingSource = new TaskCompletionSource<bool>();
            SceneManager.LoadSceneAsync(TransitionSceneName, LoadSceneMode.Additive).completed += (_) => sceneLoadingSource.SetResult(true);
            await sceneLoadingSource.Task;

            var scene = SceneManager.GetSceneByName(TransitionSceneName);
            while (!scene.isLoaded)
            {
                await Task.Yield();
            }

            TransitionHandle handle = null;
            foreach (var gameObject in scene.GetRootGameObjects())
            {
                handle = gameObject.GetComponent<TransitionHandle>();
                if (handle != null)
                {
                    break;
                }
            }

            if (handle == null)
            {
                throw new Exception("Failed to find transition handle in transtion scene!");
            }

            return handle;
        }
    }
}
