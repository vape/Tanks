using UnityEngine;

namespace Tanks.Game.Tank.Guns
{
    public class BasicTankGun : TankGun
    {
        [SerializeField]
        private Projectile projectile;
        [SerializeField]
        private Transform muzzle;
        [SerializeField]
        private float reloadTime;
        [SerializeField]
        private float projectileSpeed;
        [SerializeField]
        private float projectileRange;

        public override float GetPhaseDuration(GunPhase phase)
        {
            switch (phase)
            {
                case GunPhase.Reload:
                    return reloadTime;
                default:
                    return base.GetPhaseDuration(phase);
            }
        }

        protected override void Update()
        {
            base.Update();

            switch (Phase)
            {
                case GunPhase.Reload:
                    if (PhaseElapsed > reloadTime)
                    {
                        SetPhase(GunPhase.Idle);
                    }
                    break;
            }
        }

        public override bool TryFire()
        {
            if (!initialized || Phase == GunPhase.Reload)
            {
                return false;
            }

            projectiles.Create(projectile, muzzle.position, muzzle.forward, projectileSpeed, projectileRange / projectileSpeed);
            SetPhase(GunPhase.Reload);

            return true;
        }
    }
}
