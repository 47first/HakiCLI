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

            ConfigureCommands();

            BuildMaze();

            Player = new();
            Player.OnChangePosition += ShowRoomData;
            Player.OnChangePosition += ChangeCommandContextObject;
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
        }

        private void BuildMaze()
        {
            MazeBuilder builder = new();
            Maze = builder.Build(10);
        }

        private void ShowRoomData()
        {
            Logger.Log($"You enter the room, there are:\n");

            if (Maze.GetRoomAt(Player.Position) is MazeRoom room)
            {
                var westObject = room.GetObjectBySide(RoomSide.West);
                var northObject = room.GetObjectBySide(RoomSide.North);
                var southObject = room.GetObjectBySide(RoomSide.South);
                var eastObject = room.GetObjectBySide(RoomSide.East);

                Logger.Log(westObject != null ? $"West: {westObject.GetType().Name}\n" : "");
                Logger.Log(northObject != null ? $"North: {northObject.GetType().Name}\n" : "");
                Logger.Log(southObject != null ? $"South: {southObject.GetType().Name}\n" : "");
                Logger.Log(eastObject != null ? $"East: {eastObject.GetType().Name}\n" : "");
            }
        }
    }
}
