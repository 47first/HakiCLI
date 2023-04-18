namespace Runtime
{
    public class CommandContext
    {
        public object Subject { get; set; }
        public string[] Args { get; private set; }
        public string SuccessMessage { get; set; }
        public string FailureMessage { get; set; }

        public CommandContext(string[] args, object subject)
        {
            Args = args;
            Subject = subject;
            SuccessMessage = "";
            FailureMessage = "";
        }

        public bool IsResponded => string.IsNullOrEmpty(FailureMessage) == false ||
            string.IsNullOrEmpty(SuccessMessage) == false;
    }
}
