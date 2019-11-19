using System;
using System.IO;
using Common;

namespace Dcp
{
    /// <summary>
    /// Исполнитель. Выполняет задания.
    /// </summary>
    internal class Consumer : IConsumer
    {
        private readonly IErrorHandler<string> _errorHandler;
        private readonly IFileHandler<Result<ulong>> _fileHandler;
        private readonly IResultHandler<ConsumerResult> _resultFileWriter;
        private readonly IPCQueue<string> _pcQueue;

        private bool _isStopped = false;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Consumer"/>.
        /// </summary>
        /// <param name="errorHandler">Обработчик ошибок.</param>
        /// <param name="fileHandler">Обработчик файлов.</param>
        /// <param name="resultHandler">Обработчик результатов.</param>
        /// <param name="pcQueue">Очередь Producer-Consumer.</param>
        public Consumer(
            IErrorHandler<string> errorHandler,
            IFileHandler<Result<ulong>> fileHandler,
            IResultHandler<ConsumerResult> resultHandler,
            IPCQueue<string> pcQueue)
        {
            _errorHandler = errorHandler
                            ?? throw new ArgumentNullException(nameof(errorHandler));

            _fileHandler = fileHandler
                           ?? throw new ArgumentNullException(nameof(fileHandler));

            _resultFileWriter = resultHandler
                                ?? throw new ArgumentNullException(nameof(resultHandler));

            _pcQueue = pcQueue
                       ?? throw new ArgumentNullException(nameof(pcQueue));
        }

        /// <inheritdoc />
        public void Consume()
        {
            foreach (var item in _pcQueue.ItemsToConsume)
            {
                if (_isStopped)
                {
                    break;
                }

                ProcessItem(item);
            }
        }

        /// <inheritdoc />
        public void Stop()
        {
            _isStopped = true;
        }

        private void ProcessItem(string fullPath)
        {
            var numberOfCharacters = _fileHandler.HandleFile(fullPath);
            if (numberOfCharacters.IsFailure)
            {
                _errorHandler.HandleError(numberOfCharacters.Error);
                return;
            }

            HandleResult(fullPath, numberOfCharacters.Value);
        }

        private void HandleResult(string fullPath, ulong numberOfCharacters)
        {
            var fileName = Path.GetFileName(fullPath);
            var result = new ConsumerResult(fileName, numberOfCharacters);
            _resultFileWriter.HandleResult(result);
        }
    }
}
