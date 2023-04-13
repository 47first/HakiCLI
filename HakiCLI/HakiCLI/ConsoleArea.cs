using System.Numerics;

namespace Runtime
{
    public sealed class ConsoleArea
    {
        private Vector2 _size;
        private readonly List<string> _strings = new();

        public ConsoleArea(Vector2 size)
        {
            _size = size;
        }

        public void Write(string message)
        {

        }

        public void Draw()
        {
            (ConsoleColor foreColor, ConsoleColor backColor) tempColors = (Console.ForegroundColor, Console.BackgroundColor);

            Console.ForegroundColor = tempColors.foreColor;
            Console.BackgroundColor = tempColors.backColor;
        }
    }
}
