namespace Runtime
{
    public sealed class GameHost
    {
        public ILogger Logger { get; private set; }
        public IInputHost InputHost { get; private set; }
        public Maze Maze { get; private set; }
        public GameHost(IInputHost inputHost, ILogger logger)
        {
            InputHost = inputHost;
            Logger = logger;
        }
    }
}
