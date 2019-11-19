using System.Collections.Generic;

namespace Dcp
{
    /// <summary>
    /// Интерфейс очереди Producer-Consumer.
    /// </summary>
    /// <typeparam name="T">Тип элементов, находящихся в очереди.</typeparam>
    internal interface IPCQueue<T>
    {
        /// <summary>
        /// Добавить элемент в очередь.
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Элементы для исполнителя.
        /// </summary>
        IEnumerable<T> ItemsToConsume { get; }

        /// <summary>
        /// Останов работы.
        /// </summary>
        void Stop();
    }
}