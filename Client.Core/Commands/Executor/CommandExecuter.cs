namespace Client.Core.Commands.Executor
{
    internal class CommandExecuter : ICommandExecutor
    {
        private readonly ICollection<ICommand> _commands;

        public CommandExecuter(ICollection<ICommand> commands)
        {
            _commands = commands;
        }

        public bool TryExecute(string commandName)
        {
            foreach (var command in _commands)
                if (command.Specification.Name == commandName)
                {
                    command.Execute();
                    return true;
                }

            return false;
        }
    }
}
