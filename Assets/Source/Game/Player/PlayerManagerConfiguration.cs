using System;
using UnityEngine;

namespace Tanks.Game.Player
{
    [CreateAssetMenu(fileName = "Player Manager Configuration", menuName = "Tanks/Player Manager Configuration")]
    public class PlayerManagerConfiguration : ScriptableObject
    {
        [Serializable]
        public struct PlayerPreset
        {
            public string Name;
            public PlayerController Prefab;
        }

        public PlayerPreset[] Presets;

        public bool TryFindPreset(string name, out PlayerPreset preset)
        {
            for (int i = 0; i < Presets.Length; ++i)
            {
                if (Presets[i].Name == name)
                {
                    preset = Presets[i];
                    return true;
                }
            }

            preset = default;
            return false;
        }
    }
}
