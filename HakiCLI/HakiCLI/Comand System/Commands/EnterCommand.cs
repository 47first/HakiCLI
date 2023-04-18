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
                context.IsHandled = true;
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
