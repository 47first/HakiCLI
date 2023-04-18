using System.Numerics;

namespace Runtime
{
    public abstract class MazeEntity
    {
        private bool _isAlive;
        private Vector2 _prevPosition;
        private Vector2 _position;

        public event Action OnChangePosition;
        public event Action OnAlive;
        public event Action OnDead;

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                OnChangePosition?.Invoke();
            }
        }

        public Vector2 PreviousPosition => _prevPosition;

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
