using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Интерфейс очереди Producer-Consumer.
    /// </summary>
    public interface IPCQueue : IDisposable
    {
        /// <summary>
        /// Добавить делегат Action в очередь.
        /// </summary>
        /// <param name="action">Делегат Action.</param>
        /// <returns>Объект задачи.</returns>
        Task Enqueue(Action action);

        /// <summary>
        /// Добавить делегат Func в очередь.
        /// </summary>
        /// <typeparam name="T">Тип результата действия делегата Func.</typeparam>
        /// <param name="func">Делегат Func.</param>
        /// <returns>Объект задачи.</returns>
        Task<T> Enqueue<T>(Func<T> func);

        /// <summary>
        /// Запущенные задачи.
        /// </summary>
        IEnumerable<Task> RunningTasks { get; }
    }
}
