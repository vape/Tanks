using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks.UI.Transition
{
    public class TransitionHandle : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float showDuration;
        [SerializeField]
        private float hideDuration;
        [SerializeField]
        private Camera cam;

        public void ToggleTransitionCamera(bool enabled)
        {
            cam.gameObject.SetActive(enabled);
        }

        public async Task ShowAsync()
        {
            animator.SetTrigger("show");
            await Task.Delay((int)(showDuration * 1000));
        }

        public async Task HideAsync()
        {
            animator.SetTrigger("hide");
            await Task.Delay((int)(hideDuration * 1000));
        }

        public IEnumerator ShowRoutine()
        {
            animator.SetTrigger("show");
            yield return new WaitForSeconds(showDuration);
        }

        public IEnumerator HideRoutine()
        {
            animator.SetTrigger("hide");
            yield return new WaitForSeconds(hideDuration);
        }

        public async Task DestroyAsync()
        {
            var source = new TaskCompletionSource<bool>();
            SceneManager.UnloadSceneAsync(gameObject.scene.name).completed += (_) => source.SetResult(true);
            await source.Task;
        }
    }
}
