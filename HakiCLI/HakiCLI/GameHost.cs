using System.Numerics;

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

            Console.WriteLine($"distance from 0,0 to 0,1 : {Vector2.Distance(Vector2.Zero, new Vector2(0, 1))}");

            Player = new();
            Player.OnChangePosition += ShowRoomData;
        }

        public void StartGame()
        {
            Player.Position = Maze.GetRoom(0).Position;
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
