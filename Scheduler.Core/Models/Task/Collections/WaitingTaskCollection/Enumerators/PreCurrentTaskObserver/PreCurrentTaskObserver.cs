using Scheduler.Core.Models.Task.Collections.TaskCollection;

namespace Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators.PreCurrentTaskObserver
{
    internal class PreCurrentTaskObserver : IPreCurrentTaskObserver
    {
        public Task? PreCurrentTask
        {
            get
            {
                lock(_lock)
                    return _preCurrentTask;
            }
            private set
            {
                lock(_lock)
                    _preCurrentTask = value;
                PreCurrentTaskChanged?.Invoke(this, value);
            }
        }

        public event Action<IPreCurrentTaskObserver, Task?> PreCurrentTaskChanged;

        private readonly ITaskCollection _tasks;
        private readonly object _lock = new object();

        private Task? _preCurrentTask;
        private Task _lastCurrentTask;

        public PreCurrentTaskObserver(ITaskCollection tasks)
        {
            _tasks = tasks;
            Reset();
            _tasks.Added += HandleTaskAdded;
            _tasks.Removed += HandleTaskRemoved;
        }

        public void Reset()
        {
            TryGetEarliestTask(out var earliestTask);
            lock (_lock)
            {
                PreCurrentTask = earliestTask;
                _lastCurrentTask = null;
            }
        }

        private bool TryGetEarliestTask(out Task? earliestTask)
        {
            earliestTask = _tasks.OrderBy(o => o.DateTimeUtc).FirstOrDefault();
            return earliestTask is not null;
        }

        private void HandleTaskAdded(Task task)
        {
            if (task.DateTimeUtc >= (PreCurrentTask?.DateTimeUtc ?? DateTime.MaxValue) ||
                    task.DateTimeUtc <= DateTime.Now)
                return;

            PreCurrentTask = task;
        }

        private void HandleTaskRemoved(Task task)
        {
            if (task.Id != PreCurrentTask?.Id)
                return;

            lock (_lock)
            {
                if (_lastCurrentTask is not null)
                {
                    TryGetEarlierTask(_lastCurrentTask, out var earlierTask);
                    PreCurrentTask = earlierTask;
                }
            }
        }

        public bool TryMoveNextPreCurrentTask()
        {
            if (PreCurrentTask is null)
                return false;

            TryGetEarlierTask(PreCurrentTask, out var earlierTask);

            if (PreCurrentTask.Id == earlierTask?.Id)
                return false;

            lock (_lock)
            {
                _lastCurrentTask = PreCurrentTask;
                PreCurrentTask = earlierTask;
                return true;
            }
        }

        private bool TryGetEarlierTask(Task task, out Task? earlierTask)
        {
            earlierTask = _tasks
                .Where(w => w.DateTimeUtc >= task.DateTimeUtc && w.Id != task.Id)
                .OrderBy(o => o.DateTimeUtc)
                .FirstOrDefault();
            return earlierTask is not null;
        }
    }
}