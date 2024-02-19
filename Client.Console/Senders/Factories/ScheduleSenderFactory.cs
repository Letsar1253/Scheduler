using Client.Core.Senders;
using Client.Core.Senders.Factories;

namespace Client.Console.Senders.Factories
{
    public class ScheduleSenderFactory : IScheduleSenderFactory
    {
        public IScheduleSender Create() => new ScheduleSender();
    }
}
