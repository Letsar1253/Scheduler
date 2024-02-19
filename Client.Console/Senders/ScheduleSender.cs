using Client.Core.Senders;
using Scheduler.Core.Models;

namespace Client.Console.Senders
{
    internal class ScheduleSender : IScheduleSender
    {
        public void Send(Schedule schedule) => System.Console.WriteLine($"{schedule.Name}");

    }
}
