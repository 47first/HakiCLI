namespace Runtime
{
    public interface ILogger
    {
        public void Log(string message, ConsoleColor foreColor, ConsoleColor backColor);
    }

    public class Logger : ILogger
    {
        public string Logs { get; private set; }

        public void Log(string message, ConsoleColor foreColor, ConsoleColor backColor)
        {
            Logs += message;
        }
    }
}
