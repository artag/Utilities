using System;

namespace Dcp
{
    /// <summary>
    /// Программа.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Параметры из командной строки.</param>
        public static void Main(string[] args)
        {
            try
            {
                var application = ApplicationBuilder.Build();
                if (application.IsFailure)
                {
                    Console.WriteLine(application.Error);
                    return;
                }

                application.Value.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal Error.");

                Console.WriteLine($"{ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"{ex.InnerException.Message}");
                }

                Console.WriteLine("The application will be closed.");
            }
        }
    }
}
