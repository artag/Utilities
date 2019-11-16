using System;

namespace Dcp
{
    /// <summary>
    /// Журнал консоли.
    /// </summary>
    internal class ConsoleLog : IErrorHandler<string>
    {
        /// <inheritdoc />
        public void HandleError(string text)
        {
            Console.WriteLine($"{text}");
        }
    }
}
