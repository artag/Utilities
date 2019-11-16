namespace Dcp
{
    /// <summary>
    /// Интерфейс обработчика ошибок.
    /// </summary>
    /// <typeparam name="T">Тип обрабатываемой ошибки.</typeparam>
    internal interface IErrorHandler<T>
    {
        /// <summary>
        /// Обработать ошибку.
        /// </summary>
        /// <param name="error">Ошибка.</param>
        void HandleError(T error);
    }
}
