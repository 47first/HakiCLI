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

            ConsoleMatrix mainMatrix = new(new(0, 0, 30, 3));

            GameView gameView = new(mainMatrix);

            while (true)
            {
                gameView.Draw();
            }
        }
    }
}
