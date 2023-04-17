﻿namespace Runtime
{
    public sealed class GameView
    {
        private ConsoleMatrix _inGameMatrix;

        public GameView(ConsoleMatrix matrix)
        {
            _inGameMatrix = matrix;

            var firstText = new TextMatrix(new(0, 0, 30, 10));
            var secondText = new TextMatrix(new(0, 3, 30, 2));

            List<TextSpan> textSpans = new() { new("hellofdsa\nheljlj", ConsoleColor.Blue, ConsoleColor.Red),
                new("hellofdsa\nheljlj", ConsoleColor.White, ConsoleColor.Gray), new("hellofdsa\nheljlj", ConsoleColor.Red) };

            firstText.SendText(textSpans);
            //secondText.SendText(new TextSpan[] { new TextSpan("------------------------------------------------") });

            _inGameMatrix.AddChild(secondText);
            _inGameMatrix.AddChild(firstText);
        }

        public void Draw()
        {
            Console.SetCursorPosition(0,0);

            for (int y = 0; y < _inGameMatrix.Transform.Height; y++)
            {
                for (int x = 0; x < _inGameMatrix.Transform.Width; x++)
                    DrawChar(_inGameMatrix.GetChar(x, y));

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
