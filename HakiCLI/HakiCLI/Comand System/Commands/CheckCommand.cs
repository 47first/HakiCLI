namespace Runtime
{
    public sealed class CheckCommand : ICommand
    {
        private Inventory _inventory;
        public CheckCommand(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Execute(CommandContext context)
        {
            if (context.Args.Contains("check") == false ||
                context.Subject is not ICheckable checkable)
                return;

            if(checkable.Check(_inventory))
                context.SuccessMessage = $"{context.Subject.GetType().Name} checked successfully!";

            else
                context.SuccessMessage = $"There are nothing in {context.Subject.GetType().Name}...";
        }
    }
}
