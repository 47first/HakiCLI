namespace Runtime
{
    public interface ILogger
    {
        public void Log(string message, ConsoleColor foreColor, ConsoleColor backColor);
    }

    public class Logger : ILogger
    {
        public List<LogSegment> Logs { get; private set; } = new();

        public void Log(string message, ConsoleColor foreColor, ConsoleColor backColor)
        {
            Console.Write(message);
            Logs.Add(new(message, foreColor, backColor));
        }
    }

    public struct LogSegment
    {
        public string message;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;

        public LogSegment(string message, ConsoleColor foreColor, ConsoleColor backColor)
        {
            this.message = message;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }
    }
}
