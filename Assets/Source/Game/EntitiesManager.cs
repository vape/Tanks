using System;

namespace Tanks.Game
{
    public class EntitiesManager
    {
        public delegate void PlayerEventDelegate(PlayerController controller);

        public static event PlayerEventDelegate PlayerRegistered;
        public static event PlayerEventDelegate PlayerUnregistered;
        public PlayerController Player
        { get; private set; }

        public void Register(PlayerController controller)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (Player != null)
            {
                throw new Exception("Player already registered!");
            }

            Player = controller;
            PlayerRegistered?.Invoke(Player);
        }

        public void Unregister(PlayerController controller)
        {
            if (Player != controller)
            {
                Player = null;
                PlayerUnregistered?.Invoke(controller);
            }
        }
    }
}
