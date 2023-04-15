namespace Runtime
{
    public sealed class ConsoleMatrix
    {
        private readonly List<ConsoleMatrix> _children = new();
        private ConsoleChar[,] _matrix;
        public ConsoleRect Transform { get; set; }

        public ConsoleMatrix(ConsoleRect rect)
        {
            _matrix = new ConsoleChar[rect.width, rect.height];
            Transform = rect;

            Reset();
        }

        public void AddChild(ConsoleMatrix childMatrix) => _children.Add(childMatrix);

        public void RemoveChild(ConsoleMatrix childMatrix) => _children.Remove(childMatrix);

        public void SetChar(int x, int y, ConsoleChar pixel) => _matrix[x, y] = pixel;

        public ConsoleChar GetChar(int x, int y)
        {
            foreach (ConsoleMatrix childMatrix in _children)
                if(childMatrix.Transform.InRange(x, y))
                    return childMatrix.GetChar(x - childMatrix.Transform.x, y - childMatrix.Transform.y);

            return _matrix[x, y];
        }

        public void Reset()
        {
            for (int x = 0; x < _matrix.GetLength(0); x++)
            {
                for (int y = 0; y < _matrix.GetLength(1); y++)
                    _matrix[x, y] = new(' ', ConsoleColor.White, ConsoleColor.Black);
            }
        }
    }

    public struct ConsoleChar
    {
        public char symbol;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;

        public ConsoleChar(char symbol = ' ', ConsoleColor foreColor = ConsoleColor.White,
            ConsoleColor backColor = ConsoleColor.Black)
        {
            this.symbol = symbol;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }

        public bool IsEmpty() => char.IsWhiteSpace(symbol) && backColor == ConsoleColor.Black;
    }
}
