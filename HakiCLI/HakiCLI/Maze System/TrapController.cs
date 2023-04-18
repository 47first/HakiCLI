using System.Numerics;

namespace Runtime
{
    public class TrapController
    {
        private readonly List<MazeEntity> _entities = new();
        private readonly List<Trap> _traps = new();

        public void AddTrap(Trap trap)
        {
            _traps.Add(trap);

            trap.OnUsed += () => RemoveTrap(trap);
        }

        private void RemoveTrap(Trap trap) => _traps.Remove(trap);

        public void AddEntity(MazeEntity entity)
        {
            _entities.Add(entity);

            entity.OnChangeDestination += () => {
                if(GetTrapAt(entity.Destination.Position) is Trap trap)
                    trap.OnEntityStepOnTrap(entity);
            };
        }

        private Trap? GetTrapAt(Vector2 position) => _traps.FirstOrDefault(trap => trap.Position == position);
    }
}
