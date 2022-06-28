using UnityEngine;

namespace Tanks.Game.AI
{
    public abstract class EnemyMovement : MonoBehaviour
    {
        public abstract Vector3 Velocity
        { get; }

        public abstract void SetPositionImmediately(Vector3 position);
    }
}
