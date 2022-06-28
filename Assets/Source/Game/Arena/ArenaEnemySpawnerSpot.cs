using UnityEngine;

namespace Tanks.Game.Arena
{
    public class ArenaEnemySpawnerSpot : MonoBehaviour
    {
        public Transform Origin => origin;

        [SerializeField]
        private Transform origin;
    }
}
