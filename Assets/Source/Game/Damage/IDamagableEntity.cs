namespace Tanks.Game.Damage
{
    public interface IDamagableEntity
    {
        int Id
        { get; }
        float Protection
        { get; }
        float HealthCapacity
        { get; }
        float Health
        { get; set; }
        bool IsDead
        { get; set; }

        void OnDamage(float value, DamageInfo info);
        void OnDeath(DamageInfo info);
    }
}
