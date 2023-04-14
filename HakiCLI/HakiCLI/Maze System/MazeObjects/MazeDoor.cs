namespace Runtime
{
    public class MazeDoor: MazeObject
    {
        public MazeRoom ConnectedRoom { get; private set; }

        public MazeDoor(MazeRoom connectedRoom)
        {
            ConnectedRoom = connectedRoom;
        }
    }
}
