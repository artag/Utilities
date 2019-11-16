namespace Dcp
{
    /// <summary>
    /// Интерфейс обработчика результатов.
    /// </summary>
    /// <typeparam name="T">Тип результата, который необходимо обработать.</typeparam>
    internal interface IResultHandler<T>
    {
        /// <summary>
        /// Обработать результат.
        /// </summary>
        /// <param name="result">Результат, который необходимо обработать.</param>
        void HandleResult(T result);
    }
}
