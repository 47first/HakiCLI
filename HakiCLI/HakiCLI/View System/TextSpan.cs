namespace Runtime
{
    public struct TextSpan
    {
        public string text;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;

        public TextSpan(string message, ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            this.text = message;
            this.foreColor = foreColor;
            this.backColor = backColor;
        }
    }
}
