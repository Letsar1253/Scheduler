using Scheduler.Core.Models.Task.Collections.TaskCollection;

namespace Scheduler.Core.Models
{
    // модель расписания
    public class Schedule
    {
        public Schedule(Guid id, string name, ITaskCollection tasks)
        {
            Id = id;
            Name = name;
            Tasks = tasks;
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public ITaskCollection Tasks { get; }
    }
}
