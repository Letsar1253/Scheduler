using Scheduler.Core.Models.Task.Collections.TaskCollection;

namespace Scheduler.Core.Models.Task.Matrices.TaskMatrix
{
    internal interface ITaskMatrix : ICollection<ITaskCollection>
    {
        event Action<ITaskCollection> Added;

        event Action<ITaskCollection> Removed;
    }
}
