namespace Runtime
{
    public class ConsoleRect
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public event Action OnResize;

        public ConsoleRect(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;
        }

        public void SetSize(int x, int y, int width, int height)
        {
            Height = height;
            Width = width;
            X = x;
            Y = y;

            OnResize?.Invoke();
        }

        public bool InRange(int x, int y)
        {
            return x >= this.X && x < this.X + Width
                && y >= this.Y && y < this.Y + Height;
        }

        public int GetSize() => Height * Width;
    }
}
