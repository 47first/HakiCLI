namespace Runtime
{
    public sealed class FinishDoor : MazeDoor, ILockable
    {
        public FinishDoor(MazeRoom connectedRoom) : base(connectedRoom) { }

        public bool IsLocked { get; set; } = true;

        public override void EnterBy(MazeEntity entity)
        {
            if(IsLocked == false)
                entity.Destination = ConnectedRoom;
        }
    }
}
