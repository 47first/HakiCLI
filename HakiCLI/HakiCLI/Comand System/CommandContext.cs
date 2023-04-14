namespace Runtime
{
    public class CommandContext
    {
        public object Subject { get; set; }
        public string[] Args { get; private set; }
        public bool IsHandled { get; set; }
        public string FailureMessage { get; set; }

        public CommandContext(string[] args, object subject)
        {
            Args = args;
            Subject = subject;
            IsHandled = false;
            FailureMessage = "";
        }

        public bool IsResponded => IsHandled || string.IsNullOrEmpty(FailureMessage) == false;
    }
}
