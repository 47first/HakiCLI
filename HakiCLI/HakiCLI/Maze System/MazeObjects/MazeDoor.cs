namespace Runtime
{
    public class MazeDoor: MazeObject, IEnterable
    {
        public MazeRoom ConnectedRoom { get; private set; }

        public MazeDoor(MazeRoom connectedRoom)
        {
            ConnectedRoom = connectedRoom;
        }

        public void EnterBy(MazeEntity entity)
        {
            entity.Destination = ConnectedRoom;
        }
    }
}
