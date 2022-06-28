using System;
using UnityEngine;

namespace Tanks.Game.AI
{
    [CreateAssetMenu(fileName = "Enemies Config", menuName = "Tanks/Enemies/Enemies Config")]
    public class EnemiesConfig : ScriptableObject
    {
        [Serializable]
        public struct Preset
        {
            public string Name;
            public EnemyController Prefab;
        }

        public Preset[] Presets;

        public bool TryFind(string name, out Preset preset)
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
