namespace Dcp
{
    /// <summary>
    /// Интерфейс исполнителя (Consumer).
    /// </summary>
    /// <typeparam name="T">Тип входных данных.</typeparam>
    internal interface IConsumer<T>
    {
        /// <summary>
        /// Запуск задания.
        /// </summary>
        /// <param name="input">Входные данные.</param>
        void Work(T input);
    }
}
