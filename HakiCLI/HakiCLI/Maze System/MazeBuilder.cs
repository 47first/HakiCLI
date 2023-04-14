using System.Numerics;

namespace Runtime
{
    public sealed class MazeBuilder
    {
        public Maze Build(int roomsCount)
        {
            var maze = new Maze();

            var currentPosition = new Vector2(0, 0);
            var prevRoom = new MazeRoom(currentPosition);

            for (int buildedRooms = 0; buildedRooms < roomsCount;)
            {
                currentPosition += GetRandomDirection();

                if (maze.ContainsRoomAt(currentPosition) == false)
                {
                    buildedRooms++;
                    maze.AddRoom(new MazeRoom(currentPosition));
                }

                var curRoom = maze.Rooms.First(room => room.Position == currentPosition);

                ConnectRooms(prevRoom, curRoom);

                prevRoom = curRoom;
            }

            return maze;
        }

        private void ConnectRooms(MazeRoom roomA, MazeRoom roomB)
        {
            if (roomA is null || roomB is null)
                throw new ArgumentNullException("Room is null");

            var roomASide = GetRelativeSide(roomA.Position, roomB.Position);
            var roomBSide = GetRelativeSide(roomB.Position, roomA.Position);

            roomA.TryAddMazeObject(roomASide, new MazeDoor(roomB));
            roomB.TryAddMazeObject(roomBSide, new MazeDoor(roomA));
        }

        private RoomSide GetRelativeSide(Vector2 posFrom, Vector2 posTo)
        {
            var relativePosition = posTo - posFrom;

            if (relativePosition == new Vector2(1, 0))
                return RoomSide.Right;

            if (relativePosition == new Vector2(-1, 0))
                return RoomSide.Left;

            if (relativePosition == new Vector2(0, 1))
                return RoomSide.Forward;

            if (relativePosition == new Vector2(0, -1))
                return RoomSide.Backward;

            throw new ArgumentException($"{posTo} - {posFrom} = {relativePosition}");
        }

        private Vector2 GetRandomDirection()
        {
            Random rnd = new();

            var isNewDirByX = rnd.Next(2) == 1;
            var isDirPositive = rnd.Next(2) == 1;

            return isNewDirByX switch
            {
                true => new Vector2(isDirPositive ? 1 : -1, 0),
                false => new Vector2(0, isDirPositive ? 1 : -1)
            };
        }
    }
}
