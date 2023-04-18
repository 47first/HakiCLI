namespace Runtime
{
    public sealed class FinishDoor: MazeObject, IEnterable, ILockable
    {
        public bool IsLocked { get; set; } = true;

        public void EnterBy(MazeEntity entity)
        {
            if (IsLocked == false)
                Console.WriteLine("You win!");
        }
    }
}
