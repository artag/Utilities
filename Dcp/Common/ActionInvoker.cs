using System;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Вызов делегата Action.
    /// </summary>
    public static class ActionInvoker
    {
        /// <summary>
        /// Вызов делегата Action с задержкой.
        /// </summary>
        /// <param name="action">Делегат Action.</param>
        /// <param name="delayTime">Задержка.</param>
        /// <returns>Возвращает <see cref="Task"/> ожидания.</returns>
        public static async Task ExecuteWithDelay(Action action, TimeSpan delayTime)
        {
            await Task.Delay(delayTime);
            action();
        }
    }
}
