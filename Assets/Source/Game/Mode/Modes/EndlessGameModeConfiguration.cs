using System;
using Tanks.Game.AI;
using UnityEngine;

namespace Tanks.Game.Mode.Modes
{
    [CreateAssetMenu(fileName = "Endless Mode Config", menuName = "Tanks/Game Mode/Endless")]
    public class EndlessGameModeConfiguration : GameModeConfiguration<EndlessGameModeConfiguration, EndlessGameMode>
    {
        public int MaxEnemies = 10;
        public EnemiesConfig Enemies;

        public override EndlessGameMode CreateGameMode(GameContext context)
        {
            return new EndlessGameMode(context, this);
        }
    }
}
