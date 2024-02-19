namespace Scheduler.Core.Models.Task
{
    // модель задачи
    public class Task
    {
        public Task(Guid id, string name, DateTime dateTimeUtc)
        {
            Id = id;
            Name = name;
            DateTimeUtc = dateTimeUtc;
        }

        public Guid Id { get; }

        public string Name { get; }

        public DateTime DateTimeUtc { get; }

    }
}
