using System;
using Tanks.Game.Arena;
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
            public ArenaData Data;
            public Sprite Preview;
        }

        public ArenaPreset[] ArenaPresets;
    }
}
