using System;
using UnityEngine;

namespace Tanks.UI
{
    [CreateAssetMenu(fileName = "Main Menu Config", menuName = "Tanks/UI/Main Menu Config")]
    public class MainMenuConfig : ScriptableObject
    {
        [Serializable]
        public struct ArenaPreset
        {
            public string DisplayName;
            public string SceneName;
            public Sprite Preview;
        }

        public ArenaPreset[] ArenaPresets;
    }
}
