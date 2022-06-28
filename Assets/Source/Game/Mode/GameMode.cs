using System;
using Tanks.Game.Arena;

namespace Tanks.Game.Mode
{
    public abstract class GameMode
    {
        protected ArenaController arena;

        public virtual void OnArenaLoaded(ArenaController arena)
        {
            this.arena = arena;
        }

        public virtual void OnArenaUnloaded(ArenaController arena)
        {
            this.arena = null;
        }

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
