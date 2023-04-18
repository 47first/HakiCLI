namespace Runtime
{
    public interface ILogger
    {
        public void Log(string log);
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
