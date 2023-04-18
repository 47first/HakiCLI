namespace Runtime
{
    public sealed class FinishDoor: MazeObject, IEnterable, ILockable
    {
        private Action _finishGame;
        public FinishDoor(Action finishGame)
        {
            _finishGame = finishGame;
        }

        public bool IsLocked { get; set; } = true;

        public void EnterBy(MazeEntity entity)
        {
            if (IsLocked == false)
                _finishGame();
        }
    }
}
