using UnityEngine;

namespace Tanks.Game
{
    [RequireComponent(typeof(Camera))]
    public class PlayerCameraFollower : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private Vector3 offset;
        [SerializeField]
        private float smoothness;

        private Vector3 velocity;

        private void LateUpdate()
        {
            if (player != null)
            {
                UpdatePositionAndRotation();
            }
        }

        private void UpdatePositionAndRotation()
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, smoothness);
            transform.LookAt(player.transform.position);
        }
    }
}
