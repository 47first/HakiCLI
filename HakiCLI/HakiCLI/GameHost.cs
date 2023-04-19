using System.Numerics;

namespace Runtime
{
    public enum GameState
    {
        InProgress,
        GameOver,
        Win
    }

    public sealed class GameHost
    {
        public GameState State { get; private set; }

        public PlayerInput PlayerInput { get; private set; }
        public CommandHost CommandHost { get; private set; }

        public IInputHost InputHost { get; private set; }
        public ILogger Logger { get; private set; }

        public TrapController TrapController { get; private set; }
        public Player Player { get; private set; }
        public Enemy Enemy { get; private set; }
        public Maze Maze { get; private set; }

        public GameHost(IInputHost inputHost, ILogger logger)
        {
            TrapController = new();

            CommandHost = new CommandHost(this);
            InputHost = inputHost;
            Logger = logger;

            PlayerInput = new(InputHost, CommandHost);

            BuildMaze();

            Player = new();
            Player.OnChangeDestination += PlayerChangeDestination;
            Player.OnDead += GameOver;
            Player.Inventory.OnNewItems += (newItem, amount) => Logger.Log($"You have new items: {newItem.name} x{amount}");
            Player.Inventory.OnRemoveItems += (newItem, amount) => Logger.Log($"Items have removed: {newItem.name} x{amount}");

            Enemy = new(Logger);
            Enemy.Inventory.AddItem(new("Key"), 1);
            Enemy.OnDead += MentionDropNoise;
            Enemy.OnChangeDestination += EnemyChangeDestination;

            TrapController.AddEntity(Enemy);
            TrapController.AddEntity(Player);

            ConfigureCommands();

            CommandHost.SetContextObject(Player);
        }

        private void GameOver()
        {
            State = GameState.GameOver;

            Logger.Log("Player Dead!");

            Enemy.Kill();
        }

        private void Win()
        {
            State = GameState.Win;

            Logger.Log("You Win!");

            Enemy.Kill();
        }

        private void EnemyChangeDestination()
        {
            MentionNoise();

            if (Enemy.Destination == Player.Destination)
                Logger.Log("Maniac now in your room!");
        }

        private void BuildMaze()
        {
            var itemsToHide = new List<GameItem>() { new("Trap material"), new("Trap material"), new("Trap material") };

            MazeBuilder builder = new();
            Maze = builder.Build(25);

            foreach (var item in itemsToHide)
            {
                var drawer = new Drawer();

                drawer.AddItem(item);

                Maze.SetRandomFreeSpace(drawer);
            }

            Maze.SetRandomFreeSpace(new Drawer());

            Maze.SetRandomFreeSpace(new Drawer());

            Maze.SetRandomFreeSpace(new FinishDoor(Win));
        }

        private void ConfigureCommands()
        {
            CommandHost.AddCommand(GenerateCraftCommand());
            CommandHost.AddCommand(new SetTrapCommand(Player, TrapController));
            CommandHost.AddCommand(new RobCommand(Player));
            CommandHost.AddCommand(new SetRoomSideObjectCommand());
            CommandHost.AddCommand(new OpenCommand(Player.Inventory));
            CommandHost.AddCommand(new CheckCommand(Player.Inventory));
            CommandHost.AddCommand(new EnterCommand(Player));
        }

        private CraftCommand GenerateCraftCommand()
        {
            var craftCommand = new CraftCommand(Player.Inventory);

            craftCommand.AddIngredient(new("Trap"), new Dictionary<GameItem, int>()
            { { new("Trap material"), 3} });

            return craftCommand;
        }

        private void PlayerChangeDestination()
        {
            ShowRoomData();
        }

        public void StartGame()
        {
            State = GameState.InProgress;

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

        private void MentionDropNoise()
        {
            if (State != GameState.InProgress)
                return;

            float distance = Vector2.Distance(Player.Destination.Position, Enemy.Destination.Position);

            if(distance == 0)
                Logger.Log($"You seen how maniac fell dead by trap...");

            if (distance == 1)
                Logger.Log($"You hear something droped in {Vector2Extensions.GetRelativeSide(Player.Destination.Position, Enemy.Destination.Position)} side...");
        }
    }
}
