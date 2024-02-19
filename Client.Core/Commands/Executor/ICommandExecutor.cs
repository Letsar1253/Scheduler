namespace Client.Core.Commands.Executor
{
    public interface ICommandExecutor
    {
        bool TryExecute(string commandName);
    }
}
