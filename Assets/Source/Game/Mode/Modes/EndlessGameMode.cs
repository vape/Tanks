using System.Collections.Generic;
using Tanks.Game.AI;

namespace Tanks.Game.Mode.Modes
{
    public class EndlessGameMode : GameMode<EndlessGameModeConfiguration>
    {
        private HashSet<EnemyController> enemiesToDespawn = new HashSet<EnemyController>();

        public EndlessGameMode(GameContext context, EndlessGameModeConfiguration configuration)
            : base(context, configuration)
        { }

        public override void Update(float delta)
        {
            base.Update(delta);

            if (context.PlayerCamera.Player == null && World.Player.Controller != null)
            {
                context.PlayerCamera.Player = World.Player.Controller.gameObject;
            }

            if (arena != null)
            {
                foreach (var enemy in World.Enemies.Enemies)
                {
                    if (enemy.Dead)
                    {
                        enemiesToDespawn.Add(enemy);
                    }
                }

                if (enemiesToDespawn.Count > 0)
                {
                    foreach (var enemy in enemiesToDespawn)
                    {
                        arena.EnemySpawner.Despawn(enemy);
                    }

                    enemiesToDespawn.Clear();
                }

                if (World.Enemies.Enemies.Count < config.MaxEnemies && arena.EnemySpawner.Spots.Count > 0)
                {
                    var enemy = config.Enemies.Presets[UnityEngine.Random.Range(0, config.Enemies.Presets.Length)].Name;
                    var spot = arena.EnemySpawner.Spots[UnityEngine.Random.Range(0, arena.EnemySpawner.Spots.Count)];

                    arena.EnemySpawner.Spawn(enemy, spot);
                }

                if (World.Player.Controller == null || World.Player.Controller.Dead)
                {
                    arena.PlayerSpawner.Respawn();
                }
            }
        }
    }
}
