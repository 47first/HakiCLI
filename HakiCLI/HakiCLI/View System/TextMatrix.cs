namespace Runtime
{
    public class TextMatrix : ConsoleMatrix
    {
        private readonly List<List<TextSpan>> _textLines = new();
        public TextMatrix(ConsoleRect rect) : base(rect) { }

        public void SendText(IEnumerable<TextSpan> textSpan, bool useOverride = true)
        {
            if(useOverride)
                _textLines.Clear();

            FillTextSpansInLines(textSpan);

            GenerateTextMatrix();
        }

        private void FillTextSpansInLines(IEnumerable<TextSpan> textSpans)
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

        private void GenerateTextMatrix()
        {
            if (IsTextMoreThanAvaliableSize())
                GenerateMatrixFromEnd();

            else
                GenerateMatrixFromStart();
        }

        private bool IsTextMoreThanAvaliableSize()
        {
            int avaliableLength = Transform.GetSize();
            int totalLength = 0;

            foreach (var line in _textLines)
            {
                foreach (var span in line)
                {
                    totalLength += span.text.Length;

                    if (totalLength >= avaliableLength)
                        return true;
                }
            }

            return false;
        }

        private void GenerateMatrixFromStart()
        {
            Reset();

            for (int y = 0; y < Transform.Height && y < _textLines.Count; y++)
            {
                int curSpanIndex = 0;
                int curSpanCharIndex = 0;

                for (int x = 0; x < Transform.Width; x++)
                {
                    if (curSpanCharIndex >= _textLines[y][curSpanIndex].text.Length)
                    {
                        curSpanCharIndex = 0;
                        curSpanIndex++;
                    }

                    if (_textLines[y].Count <= curSpanIndex)
                        break;

                    SetChar(x, y, new(
                        _textLines[y][curSpanIndex].text[curSpanCharIndex++],
                        _textLines[y][curSpanIndex].foreColor,
                        _textLines[y][curSpanIndex].backColor));
                }
            }
        }

        private void GenerateMatrixFromEnd()
        {
            GenerateMatrixFromStart();
        }
    }
}
