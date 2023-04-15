namespace Runtime
{
    public struct ConsoleChar
    {
        public static readonly ConsoleChar Empty = new(true);

        public char symbol;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;

        public bool IsEmpty { get; private set; }

        public ConsoleChar(char symbol = ' ', ConsoleColor foreColor = ConsoleColor.White,
            ConsoleColor backColor = ConsoleColor.Black)
        {
            this.symbol = symbol;
            this.foreColor = foreColor;
            this.backColor = backColor;

            IsEmpty = false;
        }

        // Creates empty char
        private ConsoleChar(bool isEmpty)
        {
            this.symbol = ' ';
            this.foreColor = ConsoleColor.White;
            this.backColor = ConsoleColor.Black;

            IsEmpty = isEmpty;
        }
    }
}
