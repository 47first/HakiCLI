namespace Runtime
{
    public interface ILogger
    {
        public void Log(TextSpan logSegment);
    }

    public class Logger : ILogger
    {
        public List<TextSpan> Logs { get; private set; } = new();

        public void Log(TextSpan logSegment) => Logs.Add(logSegment);

        public void Log(string message) => Logs.Add(new(message));
    }
}
