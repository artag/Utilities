using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Очередь Producer-Consumer.
    /// </summary>
    public class PCQueue : IPCQueue, IDisposable
    {
        private readonly BlockingCollection<Task> _taskQ = new BlockingCollection<Task>();
        private readonly List<Task> _runningTasks = new List<Task>();

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PCQueue"/>.
        /// </summary>
        /// <param name="maxNumberOfParallelTasks">
        /// Максимальное число одновременно запускаемых задач <see cref="Task"/>.
        /// </param>
        public PCQueue(int maxNumberOfParallelTasks)
        {
            for (var i = 0; i < maxNumberOfParallelTasks; i++)
            {
                Task.Factory.StartNew(Consume);
            }
        }

        /// <inheritdoc />
        public IEnumerable<Task> RunningTasks => _runningTasks;

        /// <inheritdoc />
        public Task Enqueue(Action action)
        {
            var task = new Task(action);
            _taskQ.Add(task);
            return task;
        }

        /// <inheritdoc />
        public Task<T> Enqueue<T>(Func<T> func)
        {
            var task = new Task<T>(func);
            _taskQ.Add(task);

            return task;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _taskQ.CompleteAdding();
            _taskQ.Dispose();
        }

        private void Consume()
        {
            foreach (var task in _taskQ.GetConsumingEnumerable())
            {
                if (task.IsCanceled)
                {
                    continue;
                }

                _runningTasks.Add(task);
                task.RunSynchronously();
                _runningTasks.Remove(task);
            }
        }
    }
}
