namespace Runtime
{
    public interface ICommand
    {
        public void Execute(CommandContext context);
    }
}
