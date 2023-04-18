namespace Runtime
{
    public sealed class SetTrapCommand: ICommand
    {
        private TrapController _trapController;
        private Player _player;
        public SetTrapCommand(Player player, TrapController trapController)
        {
            _trapController = trapController;
            _player = player;
        }

        public void Execute(CommandContext context)
        {
            if (context.Args.Contains("set") == false ||
                context.Args.Contains("trap") == false)
                return;

            if (_player.Inventory.GetItemAmount(new("Trap")) > 0)
            {
                _trapController.AddTrap(new(_player.Destination.Position, _player));

                _player.Inventory.RemoveItem(new("Trap"));

                context.SuccessMessage = "Trap setted successfully!";

                return;
            }

            context.FailureMessage = "Can't set trap!";
        }
    }
}
