namespace Client.Core.Senders
{
    public interface IScheduleTaskSender
    {
        void Send(Scheduler.Core.Models.Task.Task scheduleTask, TimeSpan timeOffset);
    }
}
