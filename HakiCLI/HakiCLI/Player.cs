using System.Numerics;

namespace Runtime
{
    public sealed class Player
    {
        public Vector2 Position { get; private set; }
        public Player(Vector2 position)
        {
            Position = position;
        }
    }
}
