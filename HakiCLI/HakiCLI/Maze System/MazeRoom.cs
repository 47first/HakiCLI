using System.Numerics;

namespace Runtime
{
    public enum RoomSide
    {
        Forward,
        Right,
        Backward,
        Left
    }

    public class MazeRoom: MazeObject
    {
        private readonly Dictionary<RoomSide, MazeObject> _roomSides = new();
        private readonly List<MazeEntity> _entities = new();
        private readonly Random _rnd = new();

        public Vector2 Position { get; private set; }

        public MazeRoom(Vector2 position)
        {
            Position = position;
        }

        public bool TryAddMazeObject(RoomSide side, MazeObject mazeObject) => _roomSides.TryAdd(side, mazeObject);

        public void AddEntity(MazeEntity mazeEntity)
        {
            _entities.Add(mazeEntity);

            mazeEntity.Destination = this;
        }

        public void RemoveEntity(MazeEntity mazeEntity)
        {
            _entities.Remove(mazeEntity);
        }

        public MazeObject GetObjectBySide(RoomSide side)
        {
            if (_roomSides.ContainsKey(side))
                return _roomSides[side];

            return null;
        }

        public MazeObject? GetRandomObject(Func<MazeObject, bool> predicate)
        {
            Console.WriteLine("Objects in room: " + _roomSides.Count);

            var avaliableObjects = _roomSides.Values.Where(predicate);

            if (avaliableObjects.Count() < 1)
                return null;

            return avaliableObjects.ElementAt(_rnd.Next(0, avaliableObjects.Count()));
        }

    }
}
