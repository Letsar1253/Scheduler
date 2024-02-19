using Client.Core.Receivers;
using Scheduler.Core.Logic.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Console.Receivers
{
    internal class ScheduleTaskReceiver : IScheduleTaskReceiver
    {
        private readonly IStringReceiver _stringReceiver;
        private readonly IScheduler _scheduler;

        public ScheduleTaskReceiver(IStringReceiver stringReceiver, IScheduler scheduler)
        {
            _stringReceiver = stringReceiver;
            _scheduler = scheduler;
        }

        public Scheduler.Core.Models.Task.Task Receive(Guid scheduleId)
        {
            Scheduler.Core.Models.Task.Task? scheduleTask = null;
            do
            {
                var scheduleTaskName = _stringReceiver.Receive("Введите название задания расписания");
                var scheduleTasks = _scheduler.GetScheduleTasks(scheduleId);
                scheduleTask = scheduleTasks.FirstOrDefault(f => f.Name == scheduleTaskName);
            }
            while (scheduleTask is null);

            return scheduleTask;
        }
    }
}
