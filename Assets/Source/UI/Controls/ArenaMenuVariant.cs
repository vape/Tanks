using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tanks.UI.Controls
{
    public class ArenaMenuVariant : MonoBehaviour
    {
        public enum StateType
        {
            Normal,
            Selected
        }

        public UnityEvent<MainMenuConfig.ArenaPreset> Selected;

        public StateType State => state;
        public MainMenuConfig.ArenaPreset Preset => preset;

        [SerializeField]
        private Image previewImage;
        [SerializeField]
        private TMP_Text nameDisplay;
        [SerializeField]
        private Animator animator;

        private StateType state;
        private MainMenuConfig.ArenaPreset preset;

        public void Setup(MainMenuConfig.ArenaPreset preset)
        {
            this.preset = preset;

            previewImage.sprite = preset.Preview;
            nameDisplay.text = preset.DisplayName;
        }

        public void OnSelect()
        {
            Selected?.Invoke(preset);
        }

        public void SetState(StateType state)
        {
            this.state = state;

            animator.SetBool("selected", state == StateType.Selected);
        }
    }
}
