using ScheduleTask = Scheduler.Core.Models.Task.Task;

namespace Scheduler.Core.Logic
{
    internal interface INotifyHandler
    {
        void Notify(ScheduleTask scheduleTask);
    }
}
