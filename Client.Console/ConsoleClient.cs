using Client.Console.CommandReceiver.Factory;
using Client.Core.Commands;

namespace Client.Console
{
    internal class ConsoleClient : IConsoleClient
    {
        private IConsoleCommandReceiverFactory _commandReceiverFactory;
        private CancellationTokenSource? _cancellationTokenSource;
        private bool _disposed = false;

        public ConsoleClient(IConsoleCommandReceiverFactory commandReceiverFactory)
        {
            _commandReceiverFactory = commandReceiverFactory;
        }

        ~ConsoleClient() => Dispose(false);

        public void Start(ICollection<ICommand> commands)
        {
            if (_cancellationTokenSource is not null)
                throw new Exception("Клиент уже запущен");

            _cancellationTokenSource = new CancellationTokenSource();
            var commandReceiver = _commandReceiverFactory.Create(commands);
            commandReceiver.DoReceive(_cancellationTokenSource.Token);
        }

        public void Stop()
        {
            if (_cancellationTokenSource is null)
                throw new Exception("Клиент еще не запущен");

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (IsNeededStop())
                        Stop();
                }

                _commandReceiverFactory = null;

                _disposed = true;
            }
        }

        private bool IsNeededStop() => _cancellationTokenSource is not null;
    }
}