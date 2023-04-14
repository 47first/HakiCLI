namespace Runtime
{
    public class Program
    {
        private static void Main(string[] args)
        {
            CommandHost chain = new CommandHost();
            chain.AddCommand(new SetRoomSideObjectCommand()).AddCommand(new ShowMazeObjectDataCommand());

            InputHost inputHost = new();
            Logger logger = new();
            PlayerInput playerInput = new(inputHost, logger, chain);

            GameHost gameHost = new(inputHost, logger, playerInput);

            chain.SetContextObject(gameHost.Maze.GetRoom(1));

            inputHost.Start();

            while (true)
            {

            }
        }
    }
}
