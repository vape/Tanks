using Tanks.Game.Tank.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Game.Tank
{
    public class TankTurret : MonoBehaviour
    {
        [SerializeField]
        private TankGun[] guns;

        private int activeGun;
        private bool isFiring;

        private void Start()
        {
            for (int i = 0; i < guns.Length; ++i)
            {
                guns[i].Initialize(World.Projectiles);
            }
        }

        public void OnNextWeapon(InputAction.CallbackContext context)
        {
            activeGun = (activeGun + 1) % guns.Length;
        }

        public void OnPrevWeapon(InputAction.CallbackContext context)
        {
            activeGun = activeGun == 0 ? guns.Length - 1 : activeGun - 1 % guns.Length;
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    isFiring = true;
                    break;
                case InputActionPhase.Canceled:
                    isFiring = false;
                    break;
            }
        }

        private void Update()
        {
            if (isFiring)
            {
                guns[activeGun].TryFire();
            }
        }
    }
}
