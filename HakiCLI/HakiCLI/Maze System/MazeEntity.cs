using System.Numerics;

namespace Runtime
{
    public abstract class MazeEntity
    {
        private Vector2 _prevPosition;
        private Vector2 _position;
        public event Action OnChangePosition;

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
    }
}
