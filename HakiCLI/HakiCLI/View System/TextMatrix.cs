namespace Runtime
{
    public sealed class TextMatrix : ConsoleMatrix
    {
        private TextSpan _textSpan;
        private int currentChar = 0;
        public TextMatrix(ConsoleRect rect) : base(rect)
        {
            Task.Run(() => {
                while (true)
                {
                    Thread.Sleep(100);
                    currentChar++;
                }
            });
        }

        public void SendText(TextSpan textSpan)
        {
            _textSpan = textSpan;
            currentChar = 0;
        }

        protected override ConsoleChar GetLocalChar(int x, int y)
        {
            if (x <= currentChar && _textSpan.text.Length > x)
                return new(_textSpan.text[x]);

            return ConsoleChar.Empty;
        }
    }
}
