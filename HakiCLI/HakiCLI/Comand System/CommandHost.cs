namespace Runtime
{
    public class CommandHost
    {
        private readonly List<ICommand> _commands = new();

        private object ContextObject { get; set; }

        public void SetContextObject(object contextObject) => ContextObject = contextObject;

        private GameHost _gameHost;

        public CommandHost(GameHost gameHost)
        {
            _gameHost = gameHost;
        }

        public CommandHost AddCommand(ICommand command)
        {
            _commands.Add(command);
            return this;
        }

        public void Request(string args)
        {
            if (_gameHost.State != GameState.InProgress)
                return;

            var context = new CommandContext(args.Trim().ToLower().Split(' '), ContextObject);

            foreach (var command in _commands)
            {
                if (context.IsResponded)
                    break;

                command.Execute(context);
            }

            if (string.IsNullOrEmpty(context.FailureMessage) == false)
                Console.WriteLine($"Command Failure Message: {context.FailureMessage}");

            if (string.IsNullOrEmpty(context.SuccessMessage) == false)
                Console.WriteLine($"{context.SuccessMessage}");
        }
    }
}
