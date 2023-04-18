using System.Numerics;

namespace Runtime
{
    public class Enemy: MazeEntity
    {
        private Vector2 _lastSeenPlayerDirection = new Vector2(0, 0);

        private float _lastActionTime = 0;
        private float _actionInterval = 100;

        public Enemy()
        {
            OnAlive += OnEnemyAlive;
            OnDead += OnEnemyDead;
        }

        private void OnEnemyAlive() => Time.FixedUpdate += Action;

        private void OnEnemyDead() => Time.FixedUpdate -= Action;

        public void Action()
        {
            _lastActionTime += Time.fixedUpdateRateInMilliseconds;

            if (_lastActionTime < _actionInterval)
                return;

            _lastActionTime = 0;

            Console.WriteLine("Enemy do action");

            EnterNextRoom();
        }

        private void EnterNextRoom()
        {
            MazeObject? mazeObject = null;

            //if (_lastSeenPlayerPosition != new Vector2(0, 0))
            //{
            //    mazeObject = Destination.GetObjectBySide(Vector2Extensions.GetRelativeSide(Position, _lastSeenPlayerPosition));

            //    _lastSeenPlayerPosition = new Vector2(0, 0);
            //}

            //else
                mazeObject = Destination.GetRandomObject(roomObject => roomObject is IEnterable);

            if (mazeObject is IEnterable enterable)
                enterable.EnterBy(this);
        }

        public void SetLastSeenPosition(Vector2 lastSeenPosition)
        {
            _lastSeenPlayerDirection = lastSeenPosition;
        }
    }
}
