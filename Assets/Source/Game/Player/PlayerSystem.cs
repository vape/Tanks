namespace Tanks.Game.Player
{
    public class PlayerSystem
    {
        public delegate void PlayerRegistrationDelegate(PlayerController controller);

        public event PlayerRegistrationDelegate PlayerRegistered;
        public event PlayerRegistrationDelegate PlayerUnregistered;

        public PlayerController Controller => controller;

        private PlayerController controller;

        public void Register(PlayerController controller)
        {
            if (this.controller != null)
            {
                throw new System.Exception("Player already registered!");
            }

            this.controller = controller;
            PlayerRegistered?.Invoke(this.controller);
        }

        public void Unregister(PlayerController controller)
        {
            if (this.controller == controller)
            {
                this.controller = null;
                PlayerUnregistered?.Invoke(controller);
            }
        }
    }
}
