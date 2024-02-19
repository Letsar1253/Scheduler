using Scheduler.Core.Models.Task.Collections.TaskCollection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.Models.Task.Matrices.TaskMatrix
{
    internal class TaskMatrix : ITaskMatrix
    {
        public int Count { get => _taskMatrix.Count; }
        public bool IsReadOnly { get => _taskMatrix.IsReadOnly; }

        public event Action<ITaskCollection> Added;

        public event Action<ITaskCollection> Removed;

        private readonly IList<ITaskCollection> _taskMatrix = new List<ITaskCollection>();

        public void Add(ITaskCollection tasks)
        {
            _taskMatrix.Add(tasks);
            Added?.Invoke(tasks);
        }
        
        public bool Remove(ITaskCollection tasks)
        {
            var successfully = _taskMatrix.Remove(tasks);

            if (successfully)
                Removed?.Invoke(tasks);

            return successfully;
        }

        public void Clear() => _taskMatrix.Clear();

        public bool Contains(ITaskCollection tasks) => _taskMatrix.Contains(tasks);

        public void CopyTo(ITaskCollection[] array, int arrayIndex) => _taskMatrix.CopyTo(array, arrayIndex);

        IEnumerator<ITaskCollection> IEnumerable<ITaskCollection>.GetEnumerator() => _taskMatrix.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _taskMatrix.GetEnumerator();
    }
}
