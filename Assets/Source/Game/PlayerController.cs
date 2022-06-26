using UnityEngine;

namespace Tanks.Game
{
    public class PlayerController : MonoBehaviour
    {
        private void OnEnable()
        {
            World.Entities.Register(this);
            
        }

        private void OnDisable()
        {
            World.Entities.Unregister(this);
        }
    }
}
