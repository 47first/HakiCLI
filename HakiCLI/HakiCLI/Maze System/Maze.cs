using System.Numerics;

namespace Runtime
{
    public class Maze
    {
        private readonly List<MazeRoom> _rooms = new();

        public IEnumerable<MazeRoom> Rooms => _rooms;

        public void AddRoom(MazeRoom room) => _rooms.Add(room);

        public bool ContainsRoomAtPosition(Vector2 position)
        {
            return _rooms.Where(room => room.Position == position).Count() > 0;
        }
    }
}
