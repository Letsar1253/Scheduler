namespace Client.Core.Commands.Executor.Factory
{
    public interface ICommandExecutorFactory
    {
        ICommandExecutor Create(ICollection<ICommand> commands);
    }
}
