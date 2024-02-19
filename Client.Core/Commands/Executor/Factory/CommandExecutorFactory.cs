namespace Client.Core.Commands.Executor.Factory
{
    public class CommandExecutorFactory : ICommandExecutorFactory
    {
        public ICommandExecutor Create(ICollection<ICommand> commands)
        {
            return new CommandExecuter(commands);
        }
    }
}
