using Scheduler.Core.Models.Task.Collections.TaskCollection;
using Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators;
using Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators.PreCurrentTaskObserver.Factory;
using Scheduler.Core.Models.Task.Matrices.TaskMatrix;
using System.Collections.Concurrent;

namespace Scheduler.Core.Models.Task.Matrices.WaitingTaskMatrix.Enumerators.PreCurrentTaskCollectionObserver
{
    internal class PreCurrentTaskCollectionObserver : IPreCurrentTaskObserver
    {
        public Task? PreCurrentTask 
        {
            get
            {
                lock(_lock)
                    return _preCurrentTaskObserver?.PreCurrentTask;
            }
        }

        public event Action<IPreCurrentTaskObserver, Task?> PreCurrentTaskChanged;

        // связка коллекции и его наблюдателя
        private readonly IDictionary<ITaskCollection, IPreCurrentTaskObserver> _relations = new ConcurrentDictionary<ITaskCollection, IPreCurrentTaskObserver>();
        private readonly IPreCurrentTaskObserverFactory _observerFactory;
        private readonly object _lock = new object();
        private IPreCurrentTaskObserver? _preCurrentTaskObserver;

        public PreCurrentTaskCollectionObserver(ITaskMatrix taskMatrix, IPreCurrentTaskObserverFactory observerFactory)
        {
            taskMatrix.Added += HandleTaskCollectionAdded;
            taskMatrix.Removed += HandleTaskCollectionRemoved;
            _observerFactory = observerFactory;
            foreach (var tasks in taskMatrix)
            {
                var observer = _observerFactory.Create(tasks);
                observer.PreCurrentTaskChanged += HandlePreCurrentTaskChanged;
                _relations.Add(tasks, observer);
            }
            Reset();
        }

        private void HandleTaskCollectionAdded(ITaskCollection tasks)
        {
            var observer = _observerFactory.Create(tasks);
            observer.PreCurrentTaskChanged += HandlePreCurrentTaskChanged;
            _relations.Add(tasks, observer);

            var preCurrentTask = observer.PreCurrentTask;
            if ((preCurrentTask?.DateTimeUtc ?? DateTime.MaxValue) >= (PreCurrentTask?.DateTimeUtc ?? DateTime.MaxValue) ||
                    (preCurrentTask?.DateTimeUtc ?? DateTime.MinValue) <= DateTime.Now)
                return;

            SetPreCurrentTaskObserver(observer);
        }

        private void HandleTaskCollectionRemoved(ITaskCollection tasks)
        {
            var observer = _relations[tasks];
            observer.PreCurrentTaskChanged -= HandlePreCurrentTaskChanged;
            _relations.Remove(tasks);

            var preCurrentTask = observer.PreCurrentTask;
            if (preCurrentTask?.Id != PreCurrentTask?.Id)
                return;

            TryGetEarliestTaskObserver(out var earliestTaskObserver);
            SetPreCurrentTaskObserver(earliestTaskObserver);
        }

        private void HandlePreCurrentTaskChanged(IPreCurrentTaskObserver sender, Task? task)
        {
            if ((task?.DateTimeUtc ?? DateTime.MaxValue) >= (PreCurrentTask?.DateTimeUtc ?? DateTime.MaxValue) ||
                    (task?.DateTimeUtc ?? DateTime.MaxValue) <= DateTime.Now)
                return;

            SetPreCurrentTaskObserver(sender);
        }

        public void Reset()
        {
            foreach (var observer in _relations.Values)
                observer.Reset();

            TryGetEarliestTaskObserver(out var earliestTaskObserver);
            SetPreCurrentTaskObserver(earliestTaskObserver);
        }

        private void SetPreCurrentTaskObserver(IPreCurrentTaskObserver? taskObserver)
        {
            lock (_lock)
            {
                _preCurrentTaskObserver = taskObserver;
                PreCurrentTaskChanged(this, _preCurrentTaskObserver?.PreCurrentTask);
            }
        }

        public bool TryMoveNextPreCurrentTask()
        {
            lock (_lock)
            {
                if (_preCurrentTaskObserver is null)
                    return false;

                return _preCurrentTaskObserver.TryMoveNextPreCurrentTask();
            }
        }

        private bool TryGetEarliestTaskObserver(out IPreCurrentTaskObserver? earliestTaskObserver)
        {
            earliestTaskObserver = _relations.Values
                .OrderBy(o => o.PreCurrentTask?.DateTimeUtc)
                .FirstOrDefault();
            return earliestTaskObserver is not null;
        }
    }
}
