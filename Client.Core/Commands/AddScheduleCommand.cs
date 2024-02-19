using Scheduler.Core.Logic.Scheduler;
using Scheduler.Core.Models;
using Client.Core.Receivers;
using Client.Core.Commands.Data;

namespace Client.Core.Commands
{
    public class AddScheduleCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly IScheduler _scheduler;
        private readonly IStringReceiver _stringReceiver;

        public AddScheduleCommand(IScheduler scheduler, IStringReceiver stringReceiver)
        {
            _scheduler = scheduler;
            _stringReceiver = stringReceiver;
            Specification = new CommandSpecification("AddSchedule", "Добавить расписание");
        }

        public void Execute() 
        {
            var scheduleName = _stringReceiver.Receive("Введите название расписания");
            var scheduleId = Guid.NewGuid();

            var schedule = new Schedule(scheduleId, scheduleName);
            _scheduler.AddSchedule(schedule);
        }
    }   
}
