namespace Runtime
{
    public sealed class GameHost
    {
        public PlayerInput PlayerInput { get; private set; }
        public CommandHost CommandHost { get; private set; }
        public IInputHost InputHost { get; private set; }
        public ILogger Logger { get; private set; }
        public Player Player { get; private set; }
        public Maze Maze { get; private set; }

        public GameHost(IInputHost inputHost, ILogger logger)
        {
            CommandHost = new CommandHost();
            InputHost = inputHost;
            Logger = logger;
            PlayerInput = new(InputHost, Logger, CommandHost);

            Player = new();
            Player.OnChangePosition += ShowRoomData;
            Player.OnChangePosition += ChangeCommandContextObject;

            BuildMaze();

            ConfigureCommands();
        }

        private void ChangeCommandContextObject() => CommandHost.SetContextObject(Maze.GetRoomAt(Player.Position));

        public void StartGame()
        {
            Player.Position = Maze.GetRoom(0).Position;

            CommandHost.SetContextObject(Maze.GetRoom(0));
        }

        private void ConfigureCommands()
        {
            CommandHost.AddCommand(new SetRoomSideObjectCommand());
            CommandHost.AddCommand(new ShowMazeObjectDataCommand());
            CommandHost.AddCommand(new EnterCommand(Player));
        }

        private void BuildMaze()
        {
            MazeBuilder builder = new();
            Maze = builder.Build(10);
        }

        private void ShowRoomData()
        {
            string logMessage = "You enter the room, there are:\n";
            
            if (Maze.GetRoomAt(Player.Position) is MazeRoom room)
            {
                var leftObject = room.GetObjectBySide(RoomSide.Left);
                var forwardObject = room.GetObjectBySide(RoomSide.Forward);
                var backwardObject = room.GetObjectBySide(RoomSide.Backward);
                var rightObject = room.GetObjectBySide(RoomSide.Right);

                logMessage += leftObject != null ? $"Left: {leftObject.GetType().Name}\n" : "";
                logMessage += forwardObject != null ? $"Forward: {forwardObject.GetType().Name}\n" : "";
                logMessage += backwardObject != null ? $"Backward: {backwardObject.GetType().Name}\n" : "";
                logMessage += rightObject != null ? $"Right: {rightObject.GetType().Name}\n" : "";
            }

            Logger.Log(logMessage);
        }
    }
}
