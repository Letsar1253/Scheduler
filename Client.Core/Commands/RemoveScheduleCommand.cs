using Scheduler.Core.Logic.Scheduler;
using Client.Core.Commands.Data;
using Client.Core.Receivers;

namespace Client.Core.Commands
{
    public class RemoveScheduleCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly IScheduler _scheduler;
        private readonly IScheduleReceiver _scheduleReceiver;

        public RemoveScheduleCommand(IScheduler scheduler, IScheduleReceiver scheduleReceiver)
        {
            _scheduler = scheduler;
            _scheduleReceiver = scheduleReceiver;
            Specification = new CommandSpecification("RemoveSchedule", "Удалить расписание");
        }

        public void Execute()
        {
            var schedule = _scheduleReceiver.Receive();
            var task = _scheduler.RemoveScheduleAsync(schedule.Id);
            task.Wait();
        }
    }
}
