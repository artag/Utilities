namespace Dcp
{
    /// <summary>
    /// Интерфейс приложения.
    /// </summary>
    internal interface IApplication
    {
        /// <summary>
        /// Запуск приложения.
        /// </summary>
        void Run();

        /// <summary>
        /// Останов приложения.
        /// </summary>
        void Stop();
    }
}
