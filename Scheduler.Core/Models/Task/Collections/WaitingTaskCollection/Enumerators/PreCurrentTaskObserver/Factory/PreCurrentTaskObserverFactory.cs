using Scheduler.Core.Models.Task.Collections.TaskCollection;

namespace Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators.PreCurrentTaskObserver.Factory
{
    internal class PreCurrentTaskObserverFactory : IPreCurrentTaskObserverFactory
    {
        public IPreCurrentTaskObserver Create(ITaskCollection tasks) => new PreCurrentTaskObserver(tasks);
    }
}
