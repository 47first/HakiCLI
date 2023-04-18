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
                currentPosition += Vector2Extensions.GetRandomDirection();

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

            var roomASide = Vector2Extensions.GetRelativeSide(roomA.Position, roomB.Position);
            var roomBSide = Vector2Extensions.GetRelativeSide(roomB.Position, roomA.Position);

            roomA.AddMazeObject(roomASide, new MazeDoor(roomB));
            roomB.AddMazeObject(roomBSide, new MazeDoor(roomA));
        }
    }
}
