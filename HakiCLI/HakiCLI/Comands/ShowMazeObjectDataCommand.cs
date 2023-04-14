namespace Runtime
{
    public sealed class ShowMazeObjectDataCommand : ICommand
    {
        public void Execute(CommandContext context)
        {
            context.IsHandled = true;

            if (context.Subject is MazeObject mazeObject)
                Console.WriteLine(mazeObject.GetType().Name);
        }
    }
}
