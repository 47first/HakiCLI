namespace Runtime
{
    public sealed class ConsoleMatrix
    {
        public ConsolePixel[,] Matrix { get; private set; }

        public ConsoleMatrix(int width, int height)
        {
            Matrix = new ConsolePixel[width, height];

            Reset();
        }

        public void SetPixel(int x, int y, ConsolePixel pixel)
        {
            Matrix[x, y] = pixel;
        }

        public void Draw()
        {
            Console.SetCursorPosition(0, 0);

            for (int x = 0; x < Matrix.GetLength(0); x++)
            {
                for (int y = 0; y < Matrix.GetLength(1); y++)
                {
                    Console.ForegroundColor = Matrix[x, y].foreColor;
                    Console.BackgroundColor = Matrix[x, y].backColor;
                    Console.Write(Matrix[x, y].character);
                }

                Console.WriteLine();
            }
        }

        public void Reset()
        {
            for (int x = 0; x < Matrix.GetLength(0); x++)
            {
                for (int y = 0; y < Matrix.GetLength(1); y++)
                    Matrix[x, y] = new(' ', ConsoleColor.White, ConsoleColor.Black);
            }
        }
    }

    public struct ConsolePixel
    {
        public char character;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;

        public ConsolePixel(char character, ConsoleColor foreColor, ConsoleColor backColor)
        {
            this.character = character;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }
    }
}
