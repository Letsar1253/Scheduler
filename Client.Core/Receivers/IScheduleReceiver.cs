using Scheduler.Core.Models;

namespace Client.Core.Receivers
{
    public interface IScheduleReceiver
    {
        Schedule Receive();
    }
}
