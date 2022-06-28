using Tanks.Game.Arena;
using Tanks.Game.Player;
using UnityEngine;

namespace Tanks.Game.Mode
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerCameraFollower cameraFollower;

        private GameMode mode;
        private ArenaController arena;

        private void Awake()
        {
            ArenaController.RegisterLoadingCallback(ArenaLoadedHandler);
        }

        private void OnDestroy()
        {
            ArenaController.UnregisterLoadingCallback(ArenaLoadedHandler);
            ArenaController.UnregisterUnloadingCallback(ArenaUnloadedHandler);
        }

        private void ArenaLoadedHandler(ArenaController arena)
        {
            this.arena = arena;

            if (mode != null)
            {
                mode.OnArenaLoaded(arena);
            }

            ArenaController.RegisterUnloadingCallback(ArenaUnloadedHandler);
        }

        private void ArenaUnloadedHandler(ArenaController arena)
        {
            this.arena = null;

            if (mode != null)
            {
                mode.OnArenaUnloaded(arena);
            }

            ArenaController.RegisterLoadingCallback(ArenaLoadedHandler);
        }

        public void InitializeGameMode(GameModeConfiguration config)
        {
            mode = config.CreateGameModeBase(CreateContext());

            if (arena != null)
            {
                mode.OnArenaLoaded(arena);
            }
        }

        public void DestroyGameMode()
        {
            if (mode == null)
            {
                return;
            }

            mode = null;
        }

        private void Update()
        {
            if (mode != null)
            {
                mode.Update(Time.deltaTime);
            }
        }

        private GameContext CreateContext()
        {
            return new GameContext()
            {
                PlayerCamera = cameraFollower
            };
        }
    }
}
