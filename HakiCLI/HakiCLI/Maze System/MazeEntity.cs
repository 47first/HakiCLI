namespace Runtime
{
    public abstract class MazeEntity
    {
        private bool _isAlive;
        private MazeRoom? _destination;

        public event Action OnChangeDestination;
        public event Action OnAlive;
        public event Action OnDead;

        public MazeRoom? Destination
        {
            get => _destination;
            set
            {
                if (_destination == value)
                    return;

                _destination = value;

                OnChangeDestination?.Invoke();
            }
        }

        public bool IsAlive
        {
            get => _isAlive;
            set
            {
                if (value == _isAlive)
                    return;

                _isAlive = value;

                if(_isAlive)
                    OnAlive?.Invoke();

                else
                    OnDead?.Invoke();
            }
        }
    }
}
