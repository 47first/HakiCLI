namespace Runtime
{
    public class Program
    {
        private static void Main(string[] args)
        {
            GameView gameView = new GameView();

            while (true)
            {
                gameView.Draw();
                Thread.Sleep(100);

                gameView._loggerArea.Write("Bebra");
            }
        }
    }
}
