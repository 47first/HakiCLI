namespace Runtime
{
    public class CommandHost
    {
        private readonly List<ICommand> _commands = new();

        private object ContextObject { get; set; }

        public void SetContextObject(object contextObject) => ContextObject = contextObject;

        public CommandHost AddCommand(ICommand command)
        {
            _commands.Add(command);
            return this;
        }

        public void Request(string args)
        {
            var context = new CommandContext(args.Trim().ToLower().Split(' '), ContextObject);

            foreach (var command in _commands)
            {
                if (context.IsResponded)
                    return;

                command.Execute(context);
            }

            if (string.IsNullOrEmpty(context.FailureMessage) == false)
                Console.WriteLine($"Command Failure Message: {context.FailureMessage}");
        }
    }
}
