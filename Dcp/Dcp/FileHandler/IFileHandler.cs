namespace Dcp
{
    /// <summary>
    /// Интерфейс обработчика файлов.
    /// </summary>
    /// <typeparam name="T">Тип результата обработки.</typeparam>
    public interface IFileHandler<T>
    {
        /// <summary>
        /// Обработать файл.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>Результат обработки файла.</returns>
        T HandleFile(string fullPath);
    }
}
