namespace Runtime
{
    public sealed class GameView
    {
        private ConsoleMatrix _mainMatrix;

        public GameView(ConsoleMatrix matrix)
        {
            _mainMatrix = matrix;

            var firstText = new TextMatrix(new(0, 0, 30, 1));
            var secondText = new TextMatrix(new(0, 2, 30, 1));

            firstText.SendText(new("hello"));
            secondText.SendText(new("world"));

            _mainMatrix.AddChild(firstText);
            _mainMatrix.AddChild(secondText);
        }

        public void Draw()
        {
            Console.SetCursorPosition(0,0);

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
