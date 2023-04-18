namespace Runtime
{
    public sealed class PlayerInput
    {
        private CommandHost _commandChain;
        private IInputHost _inputHost;
        private string _inputLine;

        public event Action OnInput;

        public PlayerInput(IInputHost inputHost, CommandHost commandChain)
        {
            _commandChain = commandChain;
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

            if (keyInfo.Key == ConsoleKey.Enter && string.IsNullOrEmpty(_inputLine) == false)
            {
                _commandChain.Request(_inputLine);
                _inputLine = "";
                return;
            }

            if (char.IsLetterOrDigit(keyInfo.KeyChar))
                _inputLine += keyInfo.KeyChar;
        }
    }
}
