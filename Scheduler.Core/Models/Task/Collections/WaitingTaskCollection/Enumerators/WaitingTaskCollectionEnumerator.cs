using System.Collections;

namespace Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators
{
    internal class WaitingTaskCollectionEnumerator : IEnumerator<Task>
    {
        public Task Current { get; private set; }
        object? IEnumerator.Current { get => Current; }

        private readonly IPreCurrentTaskObserver _observer;
        private readonly CancellationToken _cancellationToken;
        private readonly object _lock = new object();
        private bool _preCurrentTaskChanged = false;

        public WaitingTaskCollectionEnumerator(IPreCurrentTaskObserver observer, CancellationToken cancellationToken)
        {
            _observer = observer;
            _observer.PreCurrentTaskChanged += HandlePreCurrentTaskChanged;
            _cancellationToken = cancellationToken;
        }

        private void HandlePreCurrentTaskChanged(IPreCurrentTaskObserver sender, Task? preCurrentTask) 
        {
            lock(_lock )
            {
                _preCurrentTaskChanged = true;
                Monitor.PulseAll(_lock);
            }
        }

        public void Reset()
        {
            Current = null;
            _observer.Reset();
        }

        public bool MoveNext()
        {
            while (!_cancellationToken.IsCancellationRequested)
            {
                var preCurrentTask = _observer.PreCurrentTask;
                if (preCurrentTask is not null && TryWaitPreCurrentTask(preCurrentTask))
                {
                    _cancellationToken.ThrowIfCancellationRequested();
                    Current = preCurrentTask;
                    _observer.TryMoveNextPreCurrentTask();
                    return true;
                }
                else
                {
                    _cancellationToken.ThrowIfCancellationRequested();
                    Monitor.Wait(_lock);
                }
            }

            return false;
        }

        private bool TryWaitPreCurrentTask(Task preCurrentTask)
        {
            lock (_lock)
            {
                _cancellationToken.ThrowIfCancellationRequested();

                var now = DateTime.UtcNow;
                var triggerDateTime = preCurrentTask.DateTimeUtc;
                var timeUntilTrigger = triggerDateTime - now;

                if (timeUntilTrigger <= TimeSpan.Zero)
                    return true;

                return TryWaitTime(timeUntilTrigger);
            }
        }

        private bool TryWaitTime(TimeSpan waitingTime)
        {
            lock (_lock)
            {
                _preCurrentTaskChanged = false;
                Monitor.Wait(_lock, waitingTime);

                return !_preCurrentTaskChanged;
            }
        }

        public void Dispose() { }
    }
}