using Scheduler.Core.Logic.Scheduler;
using Client.Core.Senders;
using Client.Core.Commands.Data;

namespace Client.Core.Commands
{
    public class GetSchedulesCommand : ICommand
    {
        public CommandSpecification Specification { get; }

        private readonly IScheduler _scheduler;
        private readonly IScheduleSender _scheduleSender;

        public GetSchedulesCommand(IScheduler scheduler, IScheduleSender scheduleSender)
        {
            _scheduler = scheduler;
            _scheduleSender = scheduleSender;
            Specification = new CommandSpecification("GetSchedules", "Получить расписания");
        }

        public void Execute()
        {
            var schedules = _scheduler.GetSchedules();
            foreach (var schedule in schedules)
                _scheduleSender.Send(schedule);
        }
    }
}
