using Client.Core.Receivers;
using Scheduler.Core.Logic.Scheduler;
using Scheduler.Core.Models;

namespace Client.Console.Receivers
{
    internal class ScheduleReceiver : IScheduleReceiver
    {
        private readonly IStringReceiver _stringReceiver;
        private readonly IScheduler _scheduler;

        public ScheduleReceiver(IStringReceiver stringReceiver, IScheduler scheduler)
        {
            _stringReceiver = stringReceiver;
            _scheduler = scheduler;
        }

        public Schedule Receive()
        {
            Schedule? schedule = null;
            do
            {
                var scheduleName = _stringReceiver.Receive("Введите название расписания");
                var schedules = _scheduler.GetSchedules();
                schedule = schedules.FirstOrDefault(f => f.Name == scheduleName);
            }
            while (schedule is null);

            return schedule;
        }
    }
}
