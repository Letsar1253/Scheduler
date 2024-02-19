using Client.Core.Receivers;
using Client.Core.Receivers.Factories;
using Scheduler.Core.Logic.Scheduler;

namespace Client.Console.Receivers.Factories
{
    public class ScheduleReceiverFactory : IScheduleReceiverFactory
    {
        private readonly IStringReceiverFactory _stringReceiverFactory;
        private readonly IScheduler _scheduler;

        public ScheduleReceiverFactory(IStringReceiverFactory stringReceiverFactory, IScheduler scheduler)
        {
            _stringReceiverFactory = stringReceiverFactory;
            _scheduler = scheduler;
        }

        public IScheduleReceiver Create()
        {
            var stringReceiver = _stringReceiverFactory.Create();
            return new ScheduleReceiver(stringReceiver, _scheduler);
        }
    }
}
