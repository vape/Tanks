using Tanks.Game.Player;
using UnityEngine;

namespace Tanks.Game.Mode
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager playerManager;

        private GameMode mode;

        public void Load(GameModeConfiguration config)
        {
            mode = config.CreateGameModeBase(CreateContext());
        }

        public void Unload()
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
                PlayerManager = playerManager
            };
        }
    }
}
