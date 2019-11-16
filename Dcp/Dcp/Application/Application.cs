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
        private readonly IProducer<string> _producer;

        private readonly TimeSpan _delayTime;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Application"/>.
        /// </summary>
        /// <param name="settings">Настройки приложения.</param>
        /// <param name="producer">Поставщик заданий.</param>
        public Application(Settings settings, IProducer<string> producer)
        {
            _settings = settings
                        ?? throw new ArgumentNullException(nameof(settings));

            _producer = producer
                      ?? throw new ArgumentNullException(nameof(producer));

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
            _producer.Stop();
        }

        private async void OnCreated(object source, FileSystemEventArgs args)
        {
            var fullPath = args.FullPath;

            // Задержка для передачи Producer пути к обрабатываемому файлу введена для
            // предотвращения появления ошибки доступа к файлу, из-за его использования
            // другим процессом.
            await ActionInvoker.ExecuteWithDelay(() => _producer.Create(fullPath), _delayTime);
        }
    }
}
