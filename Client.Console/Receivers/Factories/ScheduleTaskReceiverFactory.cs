using Client.Core.Receivers;
using Client.Core.Receivers.Factories;
using Scheduler.Core.Logic.Scheduler;

namespace Client.Console.Receivers.Factories
{
    public class ScheduleTaskReceiverFactory : IScheduleTaskReceiverFactory
    {
        private readonly IStringReceiverFactory _stringReceiverFactory;
        private readonly IScheduler _scheduler;

        public ScheduleTaskReceiverFactory(IStringReceiverFactory stringReceiverFactory, IScheduler scheduler)
        {
            _stringReceiverFactory = stringReceiverFactory;
            _scheduler = scheduler;
        }

        public IScheduleTaskReceiver Create()
        {
            var stringReciever = _stringReceiverFactory.Create();
            return new ScheduleTaskReceiver(stringReciever, _scheduler);
        }
    }
}
