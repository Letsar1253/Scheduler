using Client.Core.Senders;

namespace Client.Console.Senders
{
    internal class ScheduleTaskSender : IScheduleTaskSender
    {
        public void Send(Scheduler.Core.Models.Task.Task scheduleTask, TimeSpan timeOffset)
        {
            System.Console.WriteLine($"{scheduleTask.Name} - {scheduleTask.DateTimeUtc + timeOffset}");
        }
    }
}
