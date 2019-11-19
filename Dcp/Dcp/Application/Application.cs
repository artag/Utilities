using System;
using System.IO;
using Common;

namespace Dcp
{
    /// <summary>
    /// Приложение.
    /// </summary>
    internal class Application : IApplication
    {
        private readonly Settings _settings;
        private readonly IPCController<string> _pcController;

        private readonly TimeSpan _delayTime;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Application"/>.
        /// </summary>
        /// <param name="settings">Настройки приложения.</param>
        /// <param name="pcController">Контроллер Producer-Consumer.</param>
        public Application(Settings settings, IPCController<string> pcController)
        {
            _settings = settings
                        ?? throw new ArgumentNullException(nameof(settings));

            _pcController = pcController
                            ?? throw new ArgumentNullException(nameof(pcController));

            _delayTime = TimeSpan.FromSeconds(_settings.DelayTimeInSec);
        }

        /// <inheritdoc />
        public void Run()
        {
            using (var watcher = new FileSystemWatcher())
            {
                watcher.Filter = _settings.ProcessedFilesExtension;
                watcher.Path = _settings.SourceDirectory;

                watcher.Created += OnCreated;

                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press 'Esc' to quit.");

                var keyInfo = Console.ReadKey(intercept: true);
                while (keyInfo.Key != ConsoleKey.Escape)
                {
                    keyInfo = Console.ReadKey(intercept: true);
                }
            }

            Stop();
        }

        /// <inheritdoc />
        public void Stop()
        {
            Console.WriteLine("Waiting for all tasks to complete.");
            _pcController.Stop();
        }

        private async void OnCreated(object source, FileSystemEventArgs args)
        {
            var fullPath = args.FullPath;

            // Задержка для передачи Producer-Consumer Controller пути к обрабатываемому файлу
            // введена для предотвращения появления ошибки доступа к файлу,
            // из-за его использования другим процессом.
            await ActionInvoker.ExecuteWithDelay(() => _pcController.Add(fullPath), _delayTime);
        }
    }
}
