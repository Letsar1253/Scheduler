using Scheduler.Core.Logic.Scheduler;
using Client.Core.Receivers;
using Client.Core.Commands.Data;

namespace Client.Core.Commands
{
    public class AddScheduleTaskCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly IScheduler _scheduler;
        private readonly IScheduleReceiver _scheduleReceiver;
        private readonly IStringReceiver _stringReceiver;

        public AddScheduleTaskCommand(IScheduler scheduler, IScheduleReceiver scheduleReceiver, IStringReceiver stringReceiver)
        {
            _scheduler = scheduler;
            _scheduleReceiver = scheduleReceiver;
            _stringReceiver = stringReceiver;
            Specification = new CommandSpecification("AddScheduleTask", "Добавить задание в расписание");
        }

        public void Execute()
        {
            var schedule = _scheduleReceiver.Receive();
            var newScheduleTask = CreateNewScheduleTask();
            _scheduler.AddScheduleTask(schedule.Id, newScheduleTask);
        }

        private Scheduler.Core.Models.Task.Task CreateNewScheduleTask()
        {
            var scheduleTaskId = Guid.NewGuid();
            var scheduleTaskName = _stringReceiver.Receive("Введите название задания");
            var scheduleTaskDateTimeUtc = GetScheduleTaskDateTimeUtc();
            var scheduleTask = new Scheduler.Core.Models.Task(scheduleTaskId, scheduleTaskName, scheduleTaskDateTimeUtc);

            return scheduleTask;
        }

        private DateTime GetScheduleTaskDateTimeUtc()
        {
            var scheduleTaskDateTimeStr = _stringReceiver.Receive("Введите дату и время уведомления задания");
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
            var scheduleTaskDateTime = DateTime.Parse(scheduleTaskDateTimeStr) - offset;

            return scheduleTaskDateTime;
        }
    }
}
