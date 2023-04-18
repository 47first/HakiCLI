using System.Numerics;

namespace Runtime
{
    public class Maze
    {
        private readonly List<MazeRoom> _rooms = new();

        public IEnumerable<MazeRoom> Rooms => _rooms;

        public void AddRoom(MazeRoom room) => _rooms.Add(room);

        public bool ContainsRoomAt(Vector2 position) => GetRoomAt(position) != null;

        public MazeRoom? GetRoomAt(Vector2 position) => _rooms.FirstOrDefault(room => room.Position == position);

        public MazeRoom GetRoom(int index) => _rooms[index];
    }
}
