using Client.Core.Commands;
using Client.Core.Commands.Data;

namespace Client.Console.Commands
{
    internal class SendAvailableCommandsCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly ICollection<ICommand> _commands;

        public SendAvailableCommandsCommand(ICollection<ICommand> commands)
        {
            _commands = commands;
            Specification = new CommandSpecification("SendAvailableCommands", "Получить доступные команды");
        }

        public void Execute()
        {
            System.Console.WriteLine("Команды:");
            foreach (var command in _commands)
                System.Console.WriteLine($"{command.Specification.Name} - {command.Specification.Description}");
        }
    }
}
