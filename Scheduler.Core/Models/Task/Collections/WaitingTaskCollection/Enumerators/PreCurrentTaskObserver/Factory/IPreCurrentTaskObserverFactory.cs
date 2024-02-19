using Scheduler.Core.Models.Task.Collections.TaskCollection;

namespace Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators.PreCurrentTaskObserver.Factory
{
    internal interface IPreCurrentTaskObserverFactory
    {
        IPreCurrentTaskObserver Create(ITaskCollection tasks);
    }
}
