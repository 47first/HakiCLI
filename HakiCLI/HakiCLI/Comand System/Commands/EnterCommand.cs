namespace Runtime
{
    public sealed class EnterCommand : ICommand
    {
        private MazeEntity _player;
        public EnterCommand(MazeEntity player)
        {
            _player = player;
        }

        public void Execute(CommandContext context)
        {
            if (context.Subject is not IEnterable enterable)
                return;

            if (ContainsEnterArg(context.Args))
            {
                if (enterable is ILockable lockable && lockable.IsLocked)
                {
                    context.FailureMessage = "Door is locked, you can't enter";
                    return;
                }

                context.SuccessMessage = "Entered successfully!";
                enterable.EnterBy(_player);
            }
        }

        private bool ContainsEnterArg(string[] args)
        {
            foreach (var arg in args)
                if (arg == "enter")
                    return true;

            return false;
        }
    }
}
