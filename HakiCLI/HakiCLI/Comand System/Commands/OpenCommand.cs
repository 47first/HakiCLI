namespace Runtime
{
    public sealed class OpenCommand : ICommand
    {
        private Inventory _inventory;
        public OpenCommand(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Execute(CommandContext context)
        {
            if (context.Args.Contains("open") == false ||
                context.Subject is not ILockable lockableObject)
                return;

            if (lockableObject.IsLocked == false)
            {
                context.FailureMessage = $"{lockableObject.GetType().Name} is opened already!";
                return;
            }

            if (lockableObject is FinishDoor)
            {
                if (_inventory.GetItemAmount(new("Key")) > 0)
                    _inventory.RemoveItem(new("Key"));

                else
                {
                    context.FailureMessage = $"Can't open {lockableObject.GetType().Name}, need a Key!";
                    return;
                }
            }

            lockableObject.IsLocked = false;
            context.SuccessMessage = $"{lockableObject.GetType().Name} opened successfully";
        }
    }
}
