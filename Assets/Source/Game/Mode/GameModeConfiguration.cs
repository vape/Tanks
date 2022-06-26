using UnityEngine;

namespace Tanks.Game.Mode
{
    public abstract class GameModeConfiguration : ScriptableObject
    {
        public abstract GameMode CreateGameModeBase(GameContext context);
    }

    public abstract class GameModeConfiguration<TConfig, TRunner> : GameModeConfiguration
        where TRunner : GameMode<TConfig>
        where TConfig : GameModeConfiguration<TConfig, TRunner>
    {
        public override GameMode CreateGameModeBase(GameContext context)
        {
            return CreateGameMode(context);
        }

        public abstract TRunner CreateGameMode(GameContext context);
    }
}
