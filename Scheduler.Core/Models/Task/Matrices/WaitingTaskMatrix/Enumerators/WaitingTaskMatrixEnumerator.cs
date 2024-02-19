using Scheduler.Core.Models.Task.Collections.TaskCollection;
using Scheduler.Core.Models.Task.Matrices.TaskMatrix;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Models.Task.Matrices.WaitingTaskMatrix.Enumerators
{
    internal class WaitingTaskMatrixEnumerator<T> : IEnumerator<Task>
        where T : ITaskCollection
    {
        private readonly ITaskMatrix<T> _taskMatrix;

        public WaitingTaskMatrixEnumerator(ITaskMatrix<T> taskMatrix)
        {
            _taskMatrix = taskMatrix;
            _taskMatrix.
        }

        private void HandleTaskCollectionAdded(T tasks)
        {

        }

        private void HandleTaskCollectionRemoved(T tasks)
        {

        }
    }
}
