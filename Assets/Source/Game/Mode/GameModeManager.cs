using Tanks.Game.Player;
using UnityEngine;

namespace Tanks.Game.Mode
{
    public class GameModeManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager playerManager;
        [SerializeField]
        private GameModeCollection gameModes;

        private GameMode mode;

        private void Awake()
        {
            mode = gameModes.Modes[0].CreateGameModeBase(CreateContext());
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
