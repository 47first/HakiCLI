namespace Runtime
{
    public sealed class GameHost
    {
        public PlayerInput PlayerInput { get; private set; }
        public IInputHost InputHost { get; private set; }
        public ILogger Logger { get; private set; }
        public Player Player { get; private set; }
        public Maze Maze { get; private set; }
        public GameHost(IInputHost inputHost, ILogger logger, PlayerInput playerInput)
        {
            InputHost = inputHost;
            Logger = logger;
            PlayerInput = playerInput;

            MazeBuilder builder = new();
            Maze = builder.Build(10);

            Player = new(Maze.GetRoom(0).Position);
        }
    }
}
