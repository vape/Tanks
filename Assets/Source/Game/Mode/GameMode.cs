using System;

namespace Tanks.Game.Mode
{
    public abstract class GameMode
    {
        public virtual void Update(float delta)
        { }

        public void Unload()
        { }
    }

    public abstract class GameMode<TConfig> : GameMode
        where TConfig : GameModeConfiguration
    {
        protected readonly GameContext context;
        protected readonly TConfig config;

        public GameMode(GameContext context, TConfig configuration)
        {
            this.context = context;
            this.config = configuration;
        }
    }
}
