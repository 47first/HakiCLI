namespace Runtime
{
    public class ConsoleMatrix
    {
        private readonly List<ConsoleMatrix> _children = new();
        private ConsoleChar[,] _matrix;
        public ConsoleRect Transform { get; set; }

        public ConsoleMatrix(ConsoleRect rect)
        {
            _matrix = new ConsoleChar[rect.Width, rect.Height];
            Transform = rect;

            Reset();
        }

        public void AddChild(ConsoleMatrix childMatrix)
        {
            childMatrix.Transform.SetMaxSize(Transform.Width, Transform.Height);

            _children.Add(childMatrix);
        }

        public void RemoveChild(ConsoleMatrix childMatrix) => _children.Remove(childMatrix);

        public void SetChar(int x, int y, ConsoleChar pixel) => _matrix[x, y] = pixel;

        public ConsoleChar GetChar(int x, int y)
        {
            foreach (ConsoleMatrix childMatrix in _children)
            {
                if (childMatrix.Transform.InRange(x, y))
                {
                    var childChar = childMatrix.GetChar(x - childMatrix.Transform.X, y - childMatrix.Transform.Y);

                    if (childChar.IsEmpty == false)
                        return childChar;
                }
            }

            return GetLocalChar(x, y);
        }

        protected virtual ConsoleChar GetLocalChar(int x, int y) => _matrix[x, y];

        public void Reset()
        {
            for (int x = 0; x < _matrix.GetLength(0); x++)
            {
                for (int y = 0; y < _matrix.GetLength(1); y++)
                    _matrix[x, y] = ConsoleChar.Empty;
            }
        }
    }
}
