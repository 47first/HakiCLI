namespace Runtime
{
    public sealed class PlayerInput
    {
        private CommandHost _commandChain;
        private IInputHost _inputHost;
        private ILogger _logger;
        private string _inputLine;
        public PlayerInput(IInputHost inputHost, ILogger logger, CommandHost commandChain)
        {
            _commandChain = commandChain;
            _logger = logger;
            _inputHost = inputHost;
            inputHost.OnPressKey += OnPlayerPressKey;
        }

        public string InputLine => _inputLine;

        private void OnPlayerPressKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Backspace && _inputLine.Length > 0)
                _inputLine = _inputLine.Remove(_inputLine.Length - 1);

            if (keyInfo.Key == ConsoleKey.Spacebar)
                _inputLine += ' ';

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                _commandChain.Request(_inputLine);
                _inputLine = "";
                return;
            }

            if (char.IsLetterOrDigit(keyInfo.KeyChar))
                _inputLine += keyInfo.KeyChar;

            _logger.Log(new($"Input: {_inputLine}\n"));
        }
    }
}
