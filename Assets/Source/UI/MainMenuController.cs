using System.Collections.Generic;
using System.Linq;
using Tanks.Game.Level;
using Tanks.Game.Mode.Modes;
using Tanks.UI.Controls;
using UnityEngine;

namespace Tanks.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private MainMenuConfig config;
        [SerializeField]
        private ArenaMenuVariant arenaTemplate;
        [SerializeField]
        private Transform arenaVariantsContainer;

        private List<ArenaMenuVariant> arenaViews = new List<ArenaMenuVariant>();

        private void OnEnable()
        {
            for (int i = 0; i < config.ArenaPresets.Length; ++i)
            {
                var instance = Instantiate(arenaTemplate, arenaVariantsContainer);
                instance.Setup(config.ArenaPresets[i]);
                instance.Selected.AddListener(ArenaSelected);

                arenaViews.Add(instance);
            }
        }

        private void OnDestroy()
        {
            foreach (var view in arenaViews)
            {
                view.Selected.RemoveListener(ArenaSelected);
                Destroy(view.gameObject);
            }

            arenaViews.Clear();
        }

        public void OnPlayClicked()
        {
            TryLoadGame();
        }

        private void ArenaSelected(MainMenuConfig.ArenaPreset preset)
        {
            foreach (var view in arenaViews)
            {
                view.SetState(preset.SceneName == view.Preset.SceneName ? ArenaMenuVariant.StateType.Selected : ArenaMenuVariant.StateType.Normal);
            }
        }

        private bool TryLoadGame()
        {
            var selectedArena = arenaViews.FirstOrDefault(v => v.State == ArenaMenuVariant.StateType.Selected);
            if (selectedArena != null)
            {
                var manifest = new LevelManifest()
                {
                    ArenaName = selectedArena.Preset.SceneName,
                    GameMode = ScriptableObject.CreateInstance<EndlessGameModeConfiguration>()
                };

                GameNavigation.LoadGame(manifest);
                return true;
            }

            return false;
        }
    }
}
