namespace Runtime
{
    public interface IInputHost
    {
        public event Action<ConsoleKeyInfo> OnPressKey;
    }

    public sealed class InputHost: IInputHost, IDisposable
    {
        public event Action<ConsoleKeyInfo> OnPressKey;
        private Task? _inputThread;

        public void Start()
        {
            if (_inputThread is not null)
                throw new InvalidOperationException("Input host already started!");

            _inputThread = Task.Run(() => {
                while(true)
                {
                    var consoleKey = Console.ReadKey();

                    lock (this)
                        OnPressKey?.Invoke(consoleKey);
                }
            });
        }

        public void Dispose()
        {
            _inputThread?.Dispose();
        }
    }
}
