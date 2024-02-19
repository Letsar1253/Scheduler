using Client.Core.Commands;

namespace Client.Console.CommandReceiver.Factory
{
    internal interface IConsoleCommandReceiverFactory
    {
        IConsoleCommandReceiver Create(ICollection<ICommand> commands);
    }
}
