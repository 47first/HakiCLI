namespace Runtime
{
    public sealed class GameView
    {
        private ConsoleMatrix _mainMatrix;

        public GameView(ConsoleMatrix matrix)
        {
            _mainMatrix = matrix;
        }

        public void Draw()
        {
            for (int y = 0; y < _mainMatrix.Transform.height; y++)
            {
                for (int x = 0; x < _mainMatrix.Transform.width; x++)
                    DrawChar(_mainMatrix.GetChar(x, y));

                Console.WriteLine();
            }
        }

        private void DrawChar(ConsoleChar consoleChar)
        {
            Console.ForegroundColor = consoleChar.foreColor;
            Console.BackgroundColor = consoleChar.backColor;
            Console.Write(consoleChar.symbol);
        }
    }
}
