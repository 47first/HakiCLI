using System.Numerics;

namespace Runtime
{
    public class Maze
    {
        private readonly Random _rnd = new();

        private readonly List<MazeRoom> _rooms = new();

        public IEnumerable<MazeRoom> Rooms => _rooms;

        public void AddRoom(MazeRoom room) => _rooms.Add(room);

        public bool ContainsRoomAt(Vector2 position) => GetRoomAt(position) != null;

        public MazeRoom? GetRoomAt(Vector2 position) => _rooms.FirstOrDefault(room => room.Position == position);

        public MazeRoom GetRoom(int index) => _rooms[index];

        public void SetRandomFreeSpace(MazeObject mazeObject)
        {
            var roomsWithFreeSpace = _rooms.Where(room => room.FreeSpace > 0);

            roomsWithFreeSpace.ElementAt(_rnd.Next(0, roomsWithFreeSpace.Count())).SetObjectInFreeSpace(mazeObject);
        }
    }
}
