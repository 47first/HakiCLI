using System.Numerics;

namespace Runtime
{
    public sealed class Trap
    {
        private MazeEntity _owner;

        public event Action OnUsed;
        public Vector2 Position { get; private set; }

        public Trap(Vector2 position, MazeEntity owner)
        {
            Position = position;
            _owner = owner;
        }

        public void OnEntityStepOnTrap(MazeEntity entity)
        {
            if (entity == _owner)
                return;

            entity.Kill();
            OnUsed?.Invoke();
        }
    }
}
