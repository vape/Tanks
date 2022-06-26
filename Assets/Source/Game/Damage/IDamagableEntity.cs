namespace Tanks.Game.Damage
{
    public interface IDamagableEntity
    {
        int Id
        { get; }
        float HealthCapacity
        { get; }
        float Health
        { get; }
        bool IsDead
        { get; }

        void Damage(float value, DamageInfo info);
    }
}
