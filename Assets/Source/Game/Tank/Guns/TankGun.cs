using UnityEngine;

namespace Tanks.Game.Tank.Guns
{
    public enum GunPhase
    {
        Idle,
        Reload
    }

    public abstract class TankGun : MonoBehaviour
    {
        public GunPhase Phase => phase;
        public float PhaseElapsed => phaseElapsed;

        private GunPhase phase;
        private float phaseElapsed;

        protected ProjectileManager projectiles;
        protected bool initialized;

        public void Initialize(ProjectileManager projectileManager)
        {
            projectiles = projectileManager;
            initialized = true;
        }

        protected virtual void Update()
        {
            phaseElapsed += Time.deltaTime;
        }

        protected void SetPhase(GunPhase phase)
        {
            this.phase = phase;
            this.phaseElapsed = 0f; 
        }

        public float GetActivePhaseDuration()
        {
            return GetPhaseDuration(phase);
        }

        public virtual float GetPhaseDuration(GunPhase phase)
        {
            return 0f;
        }

        public abstract bool TryFire();
    }
}
