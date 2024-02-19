namespace Client.Core.Senders.Factories
{
    public interface IScheduleTaskSenderFactory
    {
        IScheduleTaskSender Create();
    }
}
