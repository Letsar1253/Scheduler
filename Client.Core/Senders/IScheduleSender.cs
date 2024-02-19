using Scheduler.Core.Models;

namespace Client.Core.Senders
{
    public interface IScheduleSender
    {
        void Send(Schedule schedule);
    }
}
