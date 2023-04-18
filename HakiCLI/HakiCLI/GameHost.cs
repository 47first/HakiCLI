using System.Numerics;

namespace Runtime
{
    public enum GameState
    {
        Initialization,
        InProgress,
        Finished
    }

    public sealed class GameHost
    {
        public PlayerInput PlayerInput { get; private set; }
        public CommandHost CommandHost { get; private set; }

        public IInputHost InputHost { get; private set; }
        public ILogger Logger { get; private set; }

        public Inventory Inventory { get; private set; }
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        public Maze Maze { get; private set; }

        public GameHost(IInputHost inputHost, ILogger logger)
        {
            PlayerInput = new(InputHost, CommandHost);
            CommandHost = new CommandHost();
            InputHost = inputHost;
            Logger = logger;

            Inventory = new();

            BuildMaze();

            Player = new();
            Player.OnChangeDestination += PlayerChangeDestination;
            Player.OnDead += () => Logger.Log("Player Dead!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

            Enemy = new(Logger);
            Enemy.OnChangeDestination += EnemyChangeDestination;

            ConfigureCommands();

            CommandHost.SetContextObject(Player);
        }

        private void EnemyChangeDestination()
        {
            MentionNoise();

            if (Enemy.Destination == Player.Destination)
                Logger.Log("Maniac now in your room!");
        }

        private void BuildMaze()
        {
            MazeBuilder builder = new();
            Maze = builder.Build(10);
        }

        private void ConfigureCommands()
        {
            CommandHost.AddCommand(new SetRoomSideObjectCommand());
            CommandHost.AddCommand(new EnterCommand(Player));
        }

        private void PlayerChangeDestination()
        {
            ShowRoomData();
        }

        public void StartGame()
        {
            Player.Spawn(Maze.GetRoom(0));
            Enemy.Spawn(Maze.GetRoom(2));
        }

        private void ShowRoomData()
        {
            string logMessage = "\nYou enter the room, there are:\n";
            
            if (Player.Destination is MazeRoom room)
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

            if (Player.Destination == Enemy.Destination)
                Logger.Log($"\nAlso there was a maniac!");
        }

        private void MentionNoise()
        {
            if(Vector2.Distance(Player.Destination.Position, Enemy.Destination.Position) == 1)
                Logger.Log($"You hear noise in {Vector2Extensions.GetRelativeSide(Player.Destination.Position, Enemy.Destination.Position)} side...");
        }
    }
}
