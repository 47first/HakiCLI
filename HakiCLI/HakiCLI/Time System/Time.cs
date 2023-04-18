namespace Runtime
{
    public static class Time
    {
        public const int fixedUpdateRateInMilliseconds = 100;
        public const int updateRateInMilliseconds = 10;

        private static int _lastFixedUpdateTime = 0;

        public static event Action FixedUpdate;

        public static float SecondsUntilStart { get; private set; } = 0;
        public static float TimeSpeed { get; set; } = 1;

        static Time() => Task.Run(StartUpdateRate);

        private static void StartUpdateRate()
        {
            while (true)
            {
                Thread.Sleep(updateRateInMilliseconds);

                UpdateValues();
            }
        }

        private static void UpdateValues()
        {
            SecondsUntilStart += updateRateInMilliseconds / 1000f;
            _lastFixedUpdateTime += updateRateInMilliseconds;

            if (_lastFixedUpdateTime < fixedUpdateRateInMilliseconds)
            {
                FixedUpdate?.Invoke();
                _lastFixedUpdateTime = 0;
            }
        }
    }
}
