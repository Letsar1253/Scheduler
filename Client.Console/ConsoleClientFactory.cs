using Client.Console.CommandReceiver.Factory;
using Client.Core;
using Client.Core.Commands.Executor.Factory;

namespace Client.Console
{
    public class ConsoleClientFactory : IClientFactory
    {
        public IClient Create()
        {
            var commandExecutorFactory = new CommandExecutorFactory();
            var commandReceiverFactory = new ConsoleCommandReceiverFactory(commandExecutorFactory);
            var consoleClient = new ConsoleClient(commandReceiverFactory);
            return consoleClient;
        }
    }
}
