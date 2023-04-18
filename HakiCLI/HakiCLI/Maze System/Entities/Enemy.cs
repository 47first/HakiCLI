using System.Numerics;

namespace Runtime
{
    public class Enemy: MazeEntity
    {
        private Vector2 _nextPosition = new(0, 0);

        private float _lastActionTime = 0;
        private float _actionInterval = 100;

        private Maze _maze;

        public Enemy(Maze maze)
        {
            OnDead += OnEnemyDead;
            OnAlive += OnEnemyAlive;
            _maze = maze;
        }

        private void OnEnemyDead() => Time.FixedUpdate -= Action;

        private void OnEnemyAlive() => Time.FixedUpdate += Action;

        public void Action()
        {
            _lastActionTime += Time.fixedUpdateRateInMilliseconds;

            if (_lastActionTime < _actionInterval)
                return;

            Console.WriteLine("Enemy do action");

            _lastActionTime = 0;

            MazeObject enterableObject = null;

            //if (_nextPosition != new Vector2(0, 0))
            //{
            //    enterableObject = _curRoom.GetObjectBySide(Vector2Extensions.GetRelativeSide(Position, _nextPosition));
            //    _nextPosition = new(0, 0);
            //}

            //else

            enterableObject = _maze.GetRoomAt(Position).GetRandomObject(roomObject => roomObject is IEnterable);

            (enterableObject as IEnterable)?.EnterBy(this);
        }

        public void SetNextPosition(Vector2 nextPosition)
        {
            _nextPosition = nextPosition;
        }
    }
}
