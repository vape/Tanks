using UnityEngine;

namespace Tanks.Game.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform origin;

        public PlayerController Spawn(PlayerController prefab)
        {
            return Instantiate(prefab, origin.transform.position, origin.transform.rotation);
        }
    }
}
