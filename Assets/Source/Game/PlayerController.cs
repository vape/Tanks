using UnityEngine;

namespace Tanks.Game
{
    public class PlayerController : MonoBehaviour
    {
        private void OnEnable()
        {
            WorldEntitiesManager.Instance.RegisterPlayer(this);
        }

        private void OnDisable()
        {
            WorldEntitiesManager.Instance.UnregisterPlayer(this);
        }
    }
}
