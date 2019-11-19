using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Dcp
{
    /// <summary>
    /// Очередь Producer-Consumer.
    /// </summary>
    /// <typeparam name="T">Тип элементов, находящихся в очереди.</typeparam>
    internal class PCQueue<T> : IPCQueue<T>, IDisposable
    {
        private readonly BlockingCollection<T> _collection = new BlockingCollection<T>();

        /// <inheritdoc />
        public void Add(T item)
        {
            _collection.Add(item);
        }

        /// <inheritdoc />
        public IEnumerable<T> ItemsToConsume => _collection.GetConsumingEnumerable();

        /// <inheritdoc />
        public void Stop()
        {
            _collection.CompleteAdding();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _collection.Dispose();
        }
    }
}
