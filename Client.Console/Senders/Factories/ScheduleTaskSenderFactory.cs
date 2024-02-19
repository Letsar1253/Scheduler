using Client.Core.Senders;
using Client.Core.Senders.Factories;

namespace Client.Console.Senders.Factories
{
    public class ScheduleTaskSenderFactory : IScheduleTaskSenderFactory
    {
        public IScheduleTaskSender Create() => new ScheduleTaskSender();
    }
}
