namespace Runtime
{
    public sealed class GameView
    {
        private ConsoleMatrix _matrix;

        public GameView(ConsoleMatrix matrix)
        {
            _matrix = matrix;
        }

        public void Draw()
        {
            _matrix.Draw();
        }
    }
}
