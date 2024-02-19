using Scheduler.Core.Models;

namespace Scheduler.Core.Logic.Notifier
{
    internal interface INotifier
    {
        void DoNotify(CancellationToken cancellationToken);
    }
}
