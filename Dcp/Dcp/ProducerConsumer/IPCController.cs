namespace Dcp
{
    /// <summary>
    /// Интерфейс контроллера Producer-Consumer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IPCController<T>
    {
        /// <summary>
        /// Создание задания.
        /// </summary>
        /// <param name="inputData">Входные данные, необходимые для создания задания.</param>
        void Add(T inputData);

        /// <summary>
        /// Останов работы.
        /// </summary>
        void Stop();
    }
}
