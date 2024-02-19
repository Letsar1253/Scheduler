namespace Client.Core.Receivers
{
    public interface IScheduleTaskReceiver
    {
        Scheduler.Core.Models.Task.Task Receive(Guid scheduleId);
    }
}
