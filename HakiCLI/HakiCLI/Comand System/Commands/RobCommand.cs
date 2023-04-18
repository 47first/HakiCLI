namespace Runtime
{
    public sealed class RobCommand : ICommand
    {
        private ILogger _logger;
        private Player _player;
        public RobCommand(Player player, ILogger logger)
        {
            _player = player;
            _logger = logger;
        }

        public void Execute(CommandContext context)
        {
            if (context.Args.Contains("rob") == false)
                return;

            var target = _player.Destination.Entities.FirstOrDefault(entity => entity != _player);

            if (target is null)
            {
                context.FailureMessage = "There is nobody to rob!";
                return;
            }

            if (target.Inventory.ItemCells.Count() < 1)
            {
                context.FailureMessage = "There is nothing to rob!";
                return;
            }

            foreach (var itemCell in target.Inventory.ItemCells)
            {
                _player.Inventory.AddItem(itemCell.Key, itemCell.Value);
                target.Inventory.RemoveItem(itemCell.Key, itemCell.Value);
            }

            _logger.Log("You robed successfully!");

            context.IsHandled = true;
        }
    }
}
