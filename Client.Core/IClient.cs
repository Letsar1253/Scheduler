using Client.Core.Commands;

namespace Client.Core
{
    public interface IClient
    {
        void Start(ICollection<ICommand> commands);

        void Stop();
    }
}
