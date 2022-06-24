using UnityEngine;

namespace Tanks.Game.Tank
{
    public class TankTracksAnimator : MonoBehaviour
    {
        [SerializeField]
        private TankController tank;
        [SerializeField]
        private MeshRenderer trackMesh;
        [SerializeField]
        private Transform[] wheels;
        [SerializeField]
        private float wheelRotationSpeed = 1f;
        [SerializeField]
        private float trackRotationSpeed = 1f;
        private float time;

        private void Update()
        {
            var currentSpeed = Mathf.Sign(tank.CurrentSpeed.y) * tank.CurrentSpeed.magnitude * Time.deltaTime;
            time += currentSpeed * trackRotationSpeed;

            if (trackMesh != null)
            {
                trackMesh.sharedMaterial.SetFloat("_ExternalTime", time);
            }

            for (int i = 0; i < wheels.Length; ++i)
            {
                wheels[i].transform.rotation *= Quaternion.Euler(currentSpeed * wheelRotationSpeed, 0, 0);
            }
        }
    }
}
