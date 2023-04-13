namespace Runtime
{
    public sealed class PlayerInput
    {
        private IInputHost _inputHost;
        private string _inputLine;
        public PlayerInput(IInputHost inputHost)
        {
            _inputHost = inputHost;
            inputHost.OnPressKey += OnPlayerPressKey;
        }

        public string InputLine => _inputLine;

        private void OnPlayerPressKey(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.Backspace)
                _inputLine = _inputLine.Remove(_inputLine.Length - 1);

            if (char.IsLetterOrDigit(keyInfo.KeyChar))
                _inputLine += keyInfo.KeyChar;
        }
    }
}
