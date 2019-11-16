using System;
using System.IO;

namespace Dcp
{
    /// <summary>
    /// Запись результатов в файл.
    /// </summary>
    internal class ResultFileWriter : IResultHandler<ConsumerResult>
    {
        private readonly IErrorHandler<string> _errorHandler;
        private readonly Settings _settings;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ResultFileWriter"/>.
        /// </summary>
        /// <param name="settings">Настройки приложения.</param>
        /// <param name="errorHandler">Обработчик ошибок.</param>
        public ResultFileWriter(Settings settings, IErrorHandler<string> errorHandler)
        {
            _settings = settings
                        ?? throw new ArgumentNullException(nameof(settings));

            _errorHandler = errorHandler
                            ?? throw new ArgumentNullException(nameof(errorHandler));
        }

        /// <inheritdoc />
        public void HandleResult(ConsumerResult result)
        {
            var targetDirectory = _settings.TargetDirectory;
            if (!Directory.Exists(targetDirectory))
            {
                _errorHandler.HandleError($"The directory {targetDirectory} doesn't exists.");
                return;
            }

            var fullPath = Path.Combine(targetDirectory, result.FileName);

            try
            {
                using (var sw = new StreamWriter(fullPath, append: false))
                {
                    sw.Write(result.NumberOfCharacters);
                }
            }
            catch (Exception e)
            {
                _errorHandler.HandleError(e.Message);
            }
        }
    }
}
