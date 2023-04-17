namespace Runtime
{
    public class TextMatrix : ConsoleMatrix
    {
        private readonly List<List<TextSpan>> _textLines = new();
        public TextMatrix(ConsoleRect rect) : base(rect) { }

        public void SendText(IEnumerable<TextSpan> textSpan)
        {
            _textLines.Clear();

            SortTextSpansToLines(textSpan);

            GenerateTextMatrix(textSpan);
        }

        private void GenerateTextMatrix(IEnumerable<TextSpan> textSpans)
        {
            if (IsTextMoreThanAvaliableSize(textSpans))
                GenerateMatrixFromEnd();

            else
                GenerateMatrixFromStart();
        }

        private bool IsTextMoreThanAvaliableSize(IEnumerable<TextSpan> textSpans)
        {
            int avaliableLength = Transform.GetSize();
            int totalLength = 0;

            foreach (var span in textSpans)
            {
                totalLength += span.text.Length;

                if(totalLength >= avaliableLength)
                    return true;
            }

            return false;
        }

        private void GenerateMatrixFromStart()
        {
            for (int x = 0; x < Transform.Width; x++)
                for (int y = 0; y < Transform.Height; y++)
                    SetChar(x, y, GetConsoleCharFromLines(x, y));
        }

        private void GenerateMatrixFromEnd()
        {
            GenerateMatrixFromStart();
        }

        private ConsoleChar GetConsoleCharFromLines(int x, int y)
        {
            if (y >= _textLines.Count || x > Transform.Width || y > Transform.Height)
                return ConsoleChar.Empty;

            int curX = 0;

            foreach (var textSpan in _textLines[y])
            {
                if (curX + textSpan.text.Length - 1 < x)
                {
                    curX += textSpan.text.Length - 1;
                    continue;
                }

                return new ConsoleChar(textSpan.text[x - curX], textSpan.foreColor, textSpan.backColor);
            }

            return ConsoleChar.Empty;
        }

        private void SortTextSpansToLines(IEnumerable<TextSpan> textSpans)
        {
            if (_textLines.Count < 1)
                _textLines.Add(new());

            foreach (var textSpan in textSpans)
            {
                var splittedText = textSpan.text.Split("\n");

                _textLines.Last().Add(new(splittedText[0], textSpan.foreColor, textSpan.backColor));

                if (splittedText.Length < 1)
                    continue;

                for (int i = 1; i < splittedText.Length; i++)
                {
                    _textLines.Add(new());

                    _textLines.Last().Add(new(splittedText[i], textSpan.foreColor, textSpan.backColor));
                }
            }
        }
    }
}
