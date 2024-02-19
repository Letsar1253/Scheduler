using System.Collections;

namespace Scheduler.Core.Models.Task.Collections.TaskCollection
{
    internal class TaskCollection : ITaskCollection
    {
        public int Count { get => _tasks.Count; }
        public bool IsReadOnly { get => _tasks.IsReadOnly; }

        public event Action<Task> Added;

        public event Action<Task> Removed;

        private readonly ICollection<Task> _tasks = new List<Task>();

        public void Add(Task task)
        {
            _tasks.Add(task);
            Added?.Invoke(task);
        }

        public bool Remove(Task task)
        {
            var successfully = _tasks.Remove(task);

            if (successfully)
                Removed?.Invoke(task);

            return successfully;
        }

        public void Clear() => _tasks.Clear();

        public bool Contains(Task task) => _tasks.Contains(task);

        public void CopyTo(Task[] array, int arrayIndex) => _tasks.CopyTo(array, arrayIndex);

        IEnumerator<Task> IEnumerable<Task>.GetEnumerator() => _tasks.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _tasks.GetEnumerator();
    }
}