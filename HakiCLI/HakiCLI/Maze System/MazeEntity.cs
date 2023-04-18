namespace Runtime
{
    public abstract class MazeEntity
    {
        private bool _isAlive;
        private MazeRoom? _destination;

        public event Action OnChangeDestination;
        public event Action OnAlive;
        public event Action OnDead;

        public Inventory Inventory { get; private set; } = new();

        public MazeRoom? Destination
        {
            get => _destination;
            set
            {
                if (_destination == value || value == null)
                    return;

                _destination?.RemoveEntity(this);

                _destination = value;

                _destination.AddEntity(this);

                OnChangeDestination?.Invoke();
            }
        }

        public bool IsAlive
        {
            get => _isAlive;
            private set
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

        public void Kill() => IsAlive = false;

        public void Relive() => IsAlive = true;

        public void Spawn(MazeRoom destination)
        {
            IsAlive = true;

            Destination = destination;
        }
    }
}
