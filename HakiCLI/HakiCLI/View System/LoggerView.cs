﻿namespace Runtime
{
    public class LoggerView
    {
        private ConsoleMatrix _matrix;
        private Logger _logger;

        public LoggerView(ConsoleMatrix loggerMatrix, Logger logger)
        {
            _matrix = loggerMatrix;
            _logger = logger;
        }

        public void UpdateMatrix()
        {
            var matrixGegments = GetMatrixSegments();
        }


        private readonly List<LogSegment> _matrixSegments = new();
        private IEnumerable<LogSegment> GetMatrixSegments()
        {
            _matrixSegments.Clear();

            int avaliableChars = 0; //_matrix.Matrix.GetLength(0) * _matrix.Matrix.GetLength(1);
            int charsCounter = 0;

            foreach (var log in _logger.Logs)
            {
                _matrixSegments.Add(log);
                charsCounter += log.message.Length;

                if (charsCounter >= avaliableChars)
                    break;
            }

            return _matrixSegments;
        }
    }
}
