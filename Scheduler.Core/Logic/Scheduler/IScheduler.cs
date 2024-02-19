using ScheduleModel = Scheduler.Core.Models.Schedule;

namespace Scheduler.Core.Logic.Scheduler
{
    public interface IScheduler
    {
        void AddSchedule(ScheduleModel schedule);

        void RemoveSchedule(Guid scheduleId);

        IEnumerable<ScheduleModel> GetSchedules();

        ScheduleModel GetSchedule(Guid scheduleId);
    }
}
