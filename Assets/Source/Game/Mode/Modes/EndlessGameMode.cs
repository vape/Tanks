namespace Tanks.Game.Mode.Modes
{
    public class EndlessGameMode : GameMode<EndlessGameModeConfiguration>
    {
        public EndlessGameMode(GameContext context, EndlessGameModeConfiguration configuration)
            : base(context, configuration)
        { }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (context.PlayerManager.Player == null)
            {
                context.PlayerManager.Spawn("default");
            }

            if (World.Enemies.Spawners.Count > 0 && World.Enemies.Enemies.Count < config.MaxEnemies)
            {
                var spawner = UnityEngine.Random.Range(0, World.Enemies.Spawners.Count);
                World.Enemies.Spawners[spawner].Spawn();
            }

            if (context.PlayerManager.Player != null &&
                context.PlayerManager.Player.Dead)
            {
                context.PlayerManager.Respawn();
            }
        }
    }
}
