using Scheduler.Core.Models.Task.Collections.TaskCollection;
using Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators;
using System.Collections;

namespace Scheduler.Core.Models.Task.Collections.WaitingTaskCollection
{
    internal class WaitingTaskCollection : IWaitingTaskCollection
    {
        private readonly ITaskCollection _tasks;
        private readonly CancellationToken _cancellationToken;

        public WaitingTaskCollection(ITaskCollection tasks, CancellationToken cancellationToken)
        {
            _tasks = tasks;
            _cancellationToken = cancellationToken;
        }

        public IEnumerator<Task> GetEnumerator() => new WaitingTaskCollectionEnumerator(_tasks, _cancellationToken);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}