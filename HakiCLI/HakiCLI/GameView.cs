using System.Numerics;

namespace Runtime
{
    public sealed class GameView
    {
        public ConsoleArea _loggerArea;
        public ConsoleArea _inputArea;

        private int _maxWidth = 30;

        public GameView()
        {
            _loggerArea = new(new Vector2(_maxWidth, 10));
            _inputArea = new(new Vector2(_maxWidth, 1));
        }

        public void Draw()
        {
            ClearConsole();

            _loggerArea.Draw();
            Console.WriteLine();
            _inputArea.Draw();
        }

        private void ClearConsole()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < _maxWidth; j++)
                    Console.Write(" ");

                Console.WriteLine();
            }
        }
    }
}
