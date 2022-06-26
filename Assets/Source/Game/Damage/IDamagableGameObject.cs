using UnityEngine;

namespace Tanks.Game.Damage
{
    public interface IDamagableGameObject : IDamagableEntity
    {
        GameObject GameObject
        { get; }
    }
}
