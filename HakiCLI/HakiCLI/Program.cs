namespace Runtime
{
    public class Program
    {
        private static void Main(string[] args)
        {
            InputHost inputHost = new();
            Logger logger = new();
            PlayerInput playerInput = new(inputHost, logger);

            GameHost gameHost = new(inputHost, logger, playerInput);

            inputHost.Start();

            while (true)
            {
                Thread.Sleep(10);
            }
        }
    }
}
