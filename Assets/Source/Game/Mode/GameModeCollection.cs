using UnityEngine;

namespace Tanks.Game.Mode
{
    [CreateAssetMenu(fileName = "Game Mode Collection", menuName = "Tanks/Game Mode Collection")]
    public class GameModeCollection : ScriptableObject
    {
        public GameModeConfiguration[] Modes;
    }
}
