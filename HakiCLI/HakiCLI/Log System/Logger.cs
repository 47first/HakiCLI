namespace Runtime
{
    public interface ILogger
    {
        public void Log(LogSegment logSegment);
    }

    public class Logger : ILogger
    {
        public List<LogSegment> Logs { get; private set; } = new();

        public static void Log(string message) => Console.WriteLine(message);

        public void Log(LogSegment logSegment) => Logs.Add(logSegment);
    }

    public struct LogSegment
    {
        public string message;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;

        public LogSegment(string message, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            this.message = message;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }
    }
}
