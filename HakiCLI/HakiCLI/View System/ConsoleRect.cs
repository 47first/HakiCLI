namespace Runtime
{
    public struct ConsoleRect
    {
        public int x, y, width, height;
        public ConsoleRect(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            this.height = height;
            this.width = width;
            this.x = x;
            this.y = y;
        }

        public bool InRange(int x, int y)
        {
            return x >= this.x && x < this.x + width
                && y >= this.y && y < this.y + height;
        }
    }
}
