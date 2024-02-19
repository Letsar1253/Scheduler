using Scheduler.Core.Models.Task.Collections.TaskCollection;
using Scheduler.Core.Models.Task.Collections.WaitingTaskCollection;
using ThreadTask = System.Threading.Tasks.Task;

namespace Scheduler.Core.Logic.Notifier
{
    internal class Notifier : INotifier
    {
        private readonly INotifyHandler _notifyHandler;
        private readonly ITaskCollection _tasks;

        public Notifier(ITaskCollection tasks)
        {
            _tasks = tasks;
        }

        public void DoNotify(CancellationToken cancellationToken) => ThreadTask.Run(() => Notify(cancellationToken));

        private void Notify(CancellationToken cancellationToken)
        {
            var waitingTasks = new WaitingTaskCollection(_tasks, cancellationToken);
            foreach (var task in waitingTasks)
                _notifyHandler.Notify(task);
        }
    }
}
