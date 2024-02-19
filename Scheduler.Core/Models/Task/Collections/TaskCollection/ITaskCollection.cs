namespace Scheduler.Core.Models.Task.Collections.TaskCollection
{ 
    public interface ITaskCollection : ICollection<Task>
    {
        event Action<Task> Added;

        event Action<Task> Removed;
    }
}
