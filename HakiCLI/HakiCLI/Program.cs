namespace Runtime
{
    public class Program
    {
        private static void Main(string[] args)
        {
            InputHost inputHost = new();
            Logger logger = new();

            GameHost gameHost = new(inputHost, logger);

            gameHost.StartGame();

            while (true)
            {

            }
        }
    }
}
