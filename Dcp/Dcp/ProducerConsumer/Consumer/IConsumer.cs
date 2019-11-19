namespace Dcp
{
    /// <summary>
    /// Интерфейс исполнителя (Consumer).
    /// </summary>
    internal interface IConsumer
    {
        /// <summary>
        /// Исполнение задания.
        /// </summary>
        void Consume();

        /// <summary>
        /// Останов работы.
        /// </summary>
        void Stop();
    }
}
