using System;
using System.IO;
using Common;

namespace Dcp
{
    /// <summary>
    /// Исполнитель. Выполняет задания.
    /// </summary>
    internal class Consumer : IConsumer<string>
    {
        private readonly IErrorHandler<string> _errorHandler;
        private readonly IFileHandler<Result<ulong>> _fileHandler;
        private readonly IResultHandler<ConsumerResult> _resultFileWriter;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Consumer"/>.
        /// </summary>
        /// <param name="errorHandler">Обработчик ошибок.</param>
        /// <param name="fileHandler">Обработчик файлов.</param>
        /// <param name="resultHandler">Обработчик результатов.</param>
        public Consumer(
            IErrorHandler<string> errorHandler,
            IFileHandler<Result<ulong>> fileHandler,
            IResultHandler<ConsumerResult> resultHandler)
        {
            _errorHandler = errorHandler
                            ?? throw new ArgumentNullException(nameof(errorHandler));

            _fileHandler = fileHandler
                           ?? throw new ArgumentNullException(nameof(fileHandler));

            _resultFileWriter = resultHandler
                                ?? throw new ArgumentNullException(nameof(resultHandler));
        }

        /// <inheritdoc />
        public void Work(string fullPath)
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
