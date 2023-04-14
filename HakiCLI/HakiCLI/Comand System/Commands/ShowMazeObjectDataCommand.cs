namespace Runtime
{
    public sealed class ShowMazeObjectDataCommand : ICommand
    {
        public void Execute(CommandContext context)
        {
            if (context.Subject is MazeObject mazeObject)
                Console.WriteLine(mazeObject.GetType().Name);
        }
    }
}
