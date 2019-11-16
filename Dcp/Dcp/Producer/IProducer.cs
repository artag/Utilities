namespace Dcp
{
    /// <summary>
    /// Интерфейс поставщика заданий на работу (Producer).
    /// </summary>
    /// <typeparam name="T">Тип задания на работу.</typeparam>
    internal interface IProducer<T>
    {
        /// <summary>
        /// Создание задания.
        /// </summary>
        /// <param name="work">Задание на работу.</param>
        void Create(T work);

        /// <summary>
        /// Останов работы поставщика.
        /// </summary>
        void Stop();
    }
}
