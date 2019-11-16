using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace Dcp
{
    /// <summary>
    /// Поставщик. Генерирует "задания" для исполнителя(ей) Consumer.
    /// </summary>
    internal class Producer : IProducer<string>
    {
        private readonly IConsumer<string> _consumer;
        private readonly IErrorHandler<string> _errorHandler;
        private readonly IPCQueue _pcQueue;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Producer"/>.
        /// </summary>
        /// <param name="consumer">Исполнитель (Consumer).</param>
        /// <param name="errorHandler">Обработчик ошибок.</param>
        /// <param name="pcQueue">Очередь Producer-Consumer.</param>
        public Producer(
            IConsumer<string> consumer,
            IErrorHandler<string> errorHandler,
            IPCQueue pcQueue)
        {
            _consumer = consumer
                        ?? throw new ArgumentNullException(nameof(consumer));

            _errorHandler = errorHandler
                            ?? throw new ArgumentNullException(nameof(errorHandler));

            _pcQueue = pcQueue
                       ?? throw new ArgumentNullException(nameof(pcQueue));
        }

        /// <inheritdoc />
        public async void Create(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                _errorHandler.HandleError($"The file {fullPath} doesn't exists.");
                return;
            }

            try
            {
                var task = _pcQueue.Enqueue(() => _consumer.Work(fullPath));
                await task;
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(ex.Message);
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            try
            {
                Task.WaitAll(_pcQueue.RunningTasks.ToArray());
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    _errorHandler.HandleError(e.Message);
                }
            }
            
            _pcQueue.Dispose();
        }
    }
}
