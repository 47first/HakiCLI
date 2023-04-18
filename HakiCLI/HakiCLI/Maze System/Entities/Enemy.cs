namespace Runtime
{
    public class Enemy: MazeEntity
    {
        private MazeRoom _targetConjecturalDestination;
        private MazeEntity _target;
        private ILogger _logger;

        private float _lastActionTime = 0;
        private float _actionInterval = 25000;

        public Enemy(ILogger logger)
        {
            _logger = logger;

            OnAlive += OnEnemyAlive;
            OnDead += OnEnemyDead;
        }

        private void OnEnemyAlive() => Time.FixedUpdate += Action;

        private void OnEnemyDead() => Time.FixedUpdate -= Action;

        public void Action()
        {
            _lastActionTime += Time.fixedUpdateRateInMilliseconds * Time.TimeSpeed;

            if (_lastActionTime < _actionInterval)
                return;

            _lastActionTime = 0;

            if (_target is null)
            {
                UpdateTarget();

                if (_target is not null)
                    return;
            }

            if(TryKillTarget() == false)
                EnterNextRoom();
        }

        private void UpdateTarget()
        {
            _target = Destination.Entities.FirstOrDefault(entity => entity.IsAlive && entity is Player);

            if (_target is not null)
            {
                _target.OnChangeDestination += OnTargetChangeDestination;
                _logger.Log("Maniac see you!");
            }
        }

        private void OnTargetChangeDestination()
        {
            _targetConjecturalDestination = _target.Destination;

            RemoveTarget();
        }

        private void RemoveTarget()
        {
            if (_target is null)
                return;

            _target.OnChangeDestination -= OnTargetChangeDestination;

            _target = null;
        }

        private bool TryKillTarget()
        {
            if (_target is not null && _target.Destination == Destination)
            {
                _target.Kill();

                RemoveTarget();

                return true;
            }

            return false;
        }

        private void EnterNextRoom()
        {
            MazeObject? mazeObject = null;

            if (_targetConjecturalDestination is not null)
            {
                var doorSide = Vector2Extensions.GetRelativeSide(Destination.Position, _targetConjecturalDestination.Position);

                mazeObject = Destination.GetObjectBySide(doorSide);

                _targetConjecturalDestination = null;
            }

            else
                mazeObject = Destination.GetRandomObject(roomObject => roomObject is IEnterable);

            if (mazeObject is IEnterable enterable)
                enterable.EnterBy(this);

            UpdateTarget();
        }
    }
}
