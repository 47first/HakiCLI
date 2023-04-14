using System.Numerics;

namespace Runtime
{
    public enum RoomSide
    {
        North,
        East,
        South,
        West
    }

    public class MazeRoom: MazeObject
    {
        private readonly Dictionary<RoomSide, MazeObject> _roomSides = new();

        public Vector2 Position { get; private set; }

        public MazeRoom(Vector2 position)
        {
            Position = position;
        }

        public MazeObject GetObjectBySide(RoomSide side)
        {
            if(_roomSides.ContainsKey(side))
                return _roomSides[side];

            return null;
        }

        public bool TryAddMazeObject(RoomSide side, MazeObject mazeObject) => _roomSides.TryAdd(side, mazeObject);
    }
}
