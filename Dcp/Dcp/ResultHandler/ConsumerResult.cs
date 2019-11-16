namespace Dcp
{
    /// <summary>
    /// Результат выполнения задания.
    /// </summary>
    internal class ConsumerResult
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ConsumerResult"/>.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="numberOfCharacters"></param>
        public ConsumerResult(string fileName, ulong numberOfCharacters)
        {
            FileName = fileName;
            NumberOfCharacters = numberOfCharacters;
        }

        /// <summary>
        /// Имя обработанного файла.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Количество символов в обработанном файле.
        /// </summary>
        public ulong NumberOfCharacters { get; }
    }
}
