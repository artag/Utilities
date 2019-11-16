namespace Dcp
{
    /// <summary>
    /// Настройки приложения.
    /// </summary>
    internal class Settings
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Settings"/>.
        /// </summary>
        /// <param name="sourceDirectory">Путь к директории с исходными документами.</param>
        /// <param name="targetDirectory">Путь к директории с результатами.</param>
        public Settings(string sourceDirectory, string targetDirectory)
        {
            SourceDirectory = sourceDirectory;
            TargetDirectory = targetDirectory;
        }

        /// <summary>
        /// Путь к директории с исходными документами.
        /// </summary>
        public string SourceDirectory { get; }

        /// <summary>
        /// Путь к директории с результатами.
        /// </summary>
        public string TargetDirectory { get; }

        /// <summary>
        /// Расширения файлов, которые будут обрабатываться.
        /// </summary>
        public string ProcessedFilesExtension { get; } = "*.txt";

        /// <summary>
        /// Задержка в секундах. Используется для задания задержки при передачи Producer
        /// пути к обрабатываемому файлу.
        /// </summary>
        public double DelayTimeInSec { get; } = 1.0;

        /// <summary>
        /// Максимальное число одновременно запускаемых задач.
        /// </summary>
        public int MaxNumberOfParallelTasks { get; } = 4;
    }
}
