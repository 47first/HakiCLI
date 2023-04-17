﻿namespace Runtime
{
    public class ConsoleRect
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int MaxWidth { get; private set; }
        public int MaxHeight { get; private set; }

        public event Action OnResize;
        public event Action OnMaxValuesUpdated;

        public ConsoleRect(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            MaxWidth = width;
            MaxHeight = height;

            SetSize(x, y, width, height);
        }

        public void SetSize(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            Height = height <= MaxHeight ? height : MaxHeight;
            Width = width <= MaxWidth ? width : MaxWidth;
            X = x;
            Y = y;

            OnResize?.Invoke();
        }

        public void SetMaxSize(int maxWidth, int maxHeight)
        {
            MaxHeight = maxHeight;
            MaxWidth = maxWidth;

            OnMaxValuesUpdated?.Invoke();
        }

        public bool InRange(int x, int y)
        {
            return x >= this.X && x < this.X + Width
                && y >= this.Y && y < this.Y + Height;
        }

        public int GetSize() => Height * Width;
    }
}
