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
        /// <param name="inputData">Входные данные, необходимые для создания задания.</param>
        void Produce(T inputData);

        /// <summary>
        /// Останов работы.
        /// </summary>
        void Stop();
    }
}
