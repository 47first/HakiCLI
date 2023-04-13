using System.Numerics;

namespace Runtime
{
    public class Program
    {
        private static void Main(string[] args)
        {
            MazeBuilder builder = new();

            var newMaze = builder.Build(100);

            Console.WriteLine(newMaze.Rooms.Count());
        }
    }
}
