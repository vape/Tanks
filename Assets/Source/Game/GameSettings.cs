using Tanks.Game.Arena;
using Tanks.Game.Mode;
using UnityEngine;

namespace Tanks.Assets.Source.Game
{
    [CreateAssetMenu(fileName = "Game Settings", menuName = "Tanks/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public ArenaData[] Arenas;
        public GameModeConfiguration[] Modes;
    }
}
