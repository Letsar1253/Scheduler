using Scheduler.Core.Logic.Scheduler;
using Client.Core.Receivers;
using Client.Core.Commands.Data;

namespace Client.Core.Commands
{
    public class RemoveScheduleTaskCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly IScheduler _scheduler;
        private readonly IScheduleReceiver _scheduleReceiver;
        private readonly IScheduleTaskReceiver _scheduleTaskReceiver;

        public RemoveScheduleTaskCommand(IScheduler scheduler, IScheduleReceiver scheduleReceiver, IScheduleTaskReceiver scheduleTaskReceiver)
        {
            _scheduler = scheduler;
            _scheduleReceiver = scheduleReceiver;
            _scheduleTaskReceiver = scheduleTaskReceiver;
            Specification = new CommandSpecification("RemoveScheduleTask", "Удалить задание из расписания");
        }

        public void Execute()
        {
            var schedule = _scheduleReceiver.Receive();
            var scheduleTask = _scheduleTaskReceiver.Receive(schedule.Id);
            _scheduler.RemoveScheduleTask(schedule.Id, scheduleTask.Id);
        }
    }
}
