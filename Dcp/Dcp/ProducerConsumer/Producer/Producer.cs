using System;
using System.IO;

namespace Dcp
{
    /// <summary>
    /// Поставщик. Генерирует "задания" для исполнителя(ей) Consumer.
    /// </summary>
    internal class Producer : IProducer<string>
    {
        private readonly IErrorHandler<string> _errorHandler;
        private readonly IPCQueue<string> _pcQueue;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Producer"/>.
        /// </summary>
        /// <param name="errorHandler">Обработчик ошибок.</param>
        /// <param name="pcQueue">Очередь Producer-Consumer.</param>
        public Producer(IErrorHandler<string> errorHandler, IPCQueue<string> pcQueue)
        {
            _errorHandler = errorHandler
                            ?? throw new ArgumentNullException(nameof(errorHandler));

            _pcQueue = pcQueue
                       ?? throw new ArgumentNullException(nameof(pcQueue));
        }

        /// <inheritdoc />
        public void Produce(string inputData)
        {
            if (!File.Exists(inputData))
            {
                _errorHandler.HandleError($"The file {inputData} doesn't exists.");
                return;
            }

            _pcQueue.Add(inputData);
        }

        /// <inheritdoc />
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
