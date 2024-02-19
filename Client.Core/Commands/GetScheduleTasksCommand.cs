using Scheduler.Core.Logic.Scheduler;
using Client.Core.Receivers;
using Client.Core.Senders;
using Client.Core.Commands.Data;

namespace Client.Core.Commands
{
    public class GetScheduleTasksCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly IScheduler _scheduler;
        private readonly IScheduleReceiver _scheduleReceiver;
        private readonly IScheduleTaskSender _scheduleTaskSender;

        public GetScheduleTasksCommand(IScheduler scheduler, IScheduleReceiver scheduleReceiver, IScheduleTaskSender scheduleTaskSender)
        {
            _scheduler = scheduler;
            _scheduleReceiver = scheduleReceiver;
            _scheduleTaskSender = scheduleTaskSender;
            Specification = new CommandSpecification("GetScheduleTasks", "Получить задания из расписания");
        }

        public void Execute()
        {
            var schedule = _scheduleReceiver.Receive();
            var scheduleTasks = _scheduler.GetScheduleTasks(schedule.Id);
            var timeOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            foreach (var scheduleTask in scheduleTasks)
                _scheduleTaskSender.Send(scheduleTask, timeOffset);
        }
    }
}
