using Client.Core.Commands;
using Client.Core.Commands.Executor.Factory;

namespace Client.Console.CommandReceiver.Factory
{
    internal class ConsoleCommandReceiverFactory : IConsoleCommandReceiverFactory
    {
        private readonly ICommandExecutorFactory _commandExecutorFactory;

        public ConsoleCommandReceiverFactory(ICommandExecutorFactory commandExecutorFactory)
        {
            _commandExecutorFactory = commandExecutorFactory;
        }

        public IConsoleCommandReceiver Create(ICollection<ICommand> commands)
        {
            var commandExecutor = _commandExecutorFactory.Create(commands);
            return new ConsoleCommandReceiver(commandExecutor);
        }
    }
}
