using UnityEngine;

namespace Tanks.Game.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform origin;

        public PlayerController Spawn(PlayerController prefab)
        {
#if UNITY_EDITOR
            // if running from editor, this allows to place player on scene by hand
            var existingPlayer = FindObjectOfType<PlayerController>();
            if (existingPlayer != null)
            {
                return existingPlayer;
            }
#endif

            return Instantiate(prefab, origin.transform.position, origin.transform.rotation);
        }
    }
}
