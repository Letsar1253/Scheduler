using Client.Core.Commands.Data;

namespace Client.Core.Commands
{
    public interface ICommand
    {
        CommandSpecification Specification { get; }

        void Execute();
    }
}
