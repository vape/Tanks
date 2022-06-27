using Tanks.Game.Damage;
using UnityEngine;

namespace Tanks.UI.Controls
{
    public class HealthBar : MonoBehaviour
    {
        public IDamagableGameObject DamagableObject
        { get { return damagable; } }

        [SerializeField]
        private ProgressBar bar;

        private new Camera camera;
        private IDamagableGameObject damagable;
        private Vector3 offset;
        private HealthBarHelper helper;

        public void SetTarget(Camera camera, IDamagableGameObject damagable)
        {
            this.camera = camera;
            this.damagable = damagable;

            helper = damagable.GameObject.GetComponent<HealthBarHelper>();

            if (helper == null)
            {
                offset = new Vector3(0, 2.5f, 0);
            }
        }

        private void LateUpdate()
        {
            if (damagable != null)
            {
                bar.Amount = damagable.HealthCapacity <= 0 ? 0 : Mathf.Clamp01(damagable.Health / damagable.HealthCapacity);

                if (damagable.GameObject != null)
                {
                    if (camera != null)
                    {
                        // TODO: use billboard shader instead of rely on camera reference
                        // if only Canvas UI shaders weren't pain in the ass..
                        transform.rotation = camera.transform.rotation;
                    }
                    
                    transform.position = helper == null ? damagable.GameObject.transform.position + offset : helper.PositionReference.transform.position;
                }
            }
        }
    }
}
