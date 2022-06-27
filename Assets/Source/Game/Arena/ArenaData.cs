using UnityEngine;

namespace Tanks.Game.Arena
{
    [CreateAssetMenu(fileName = "Arena Preset", menuName = "Tanks/Arena/Preset")]
    public class ArenaData : ScriptableObject
    {
        public string SceneName;
    }
}
