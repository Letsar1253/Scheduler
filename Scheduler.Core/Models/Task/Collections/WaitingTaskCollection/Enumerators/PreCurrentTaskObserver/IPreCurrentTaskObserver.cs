namespace Scheduler.Core.Models.Task.Collections.WaitingTaskCollection.Enumerators
{
    internal interface IPreCurrentTaskObserver
    {
        Task? PreCurrentTask { get; }

        event Action<IPreCurrentTaskObserver, Task?> PreCurrentTaskChanged;

        void Reset();

        bool TryMoveNextPreCurrentTask();
    }
}
