using Client.Core.Commands.Executor;

namespace Client.Console.CommandReceiver
{
    internal class ConsoleCommandReceiver : IConsoleCommandReceiver
    {
        private readonly ICommandExecutor _commandExecutor;

        public ConsoleCommandReceiver(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public void DoReceive(CancellationToken cancellationToken) => Task.Run(() => Receive(cancellationToken));

        private void Receive(CancellationToken cancellationToken)
        {
            while (cancellationToken.IsCancellationRequested)
            {
                System.Console.WriteLine("Введите название команды");
                var commandName = System.Console.ReadLine();

                cancellationToken.ThrowIfCancellationRequested();

                if (commandName is null)
                    continue;

                if (!_commandExecutor.TryExecute(commandName))
                    System.Console.WriteLine("Команда не распознана");
            }
        }
    }
}
