using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemyController : MonoBehaviour
    {
        public void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
