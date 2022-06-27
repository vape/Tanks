using Tanks.Game.Tank.Guns;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Game.Tank
{
    public class TankTurret : MonoBehaviour
    {
        [SerializeField]
        private TankGun[] guns;
        [SerializeField]
        private Animator animator;

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
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    SetActiveGun((activeGun + 1) % guns.Length);
                    break;
            }
        }

        public void OnPrevWeapon(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    SetActiveGun(activeGun == 0 ? guns.Length - 1 : activeGun - 1 % guns.Length);
                    break;
            }
        }

        private void SetActiveGun(int gun)
        {
            activeGun = gun;

            if (animator != null)
            {
                animator.SetInteger("gun", gun);
                animator.SetTrigger("switch_gun");
            }
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
