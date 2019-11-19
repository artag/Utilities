using System;
using System.Threading.Tasks;

namespace Dcp
{
    /// <summary>
    /// Контроллер Producer-Consumer.
    /// </summary>
    internal class PCController : IPCController<string>
    {
        private readonly IErrorHandler<string> _errorHandler;
        private readonly IProducer<string> _producer;
        private readonly IConsumer _consumer;
        private readonly IPCQueue<string> _pcQueue;
        private readonly Settings _settings;

        private Task[] _tasks;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="PCController"/>.
        /// </summary>
        /// <param name="errorHandler">Обработчик ошибок.</param>
        /// <param name="producer">Поставщик заданий на работу.</param>
        /// <param name="consumer">Исполнитель.</param>
        /// <param name="pcQueue">Очередь Producer-Consumer.</param>
        /// <param name="settings">Настройки приложения.</param>
        public PCController(
            IErrorHandler<string> errorHandler,
            IProducer<string> producer,
            IConsumer consumer,
            IPCQueue<string> pcQueue,
            Settings settings)
        {
            _errorHandler = errorHandler
                            ?? throw new ArgumentNullException(nameof(errorHandler));

            _producer = producer
                        ?? throw new ArgumentNullException(nameof(producer));

            _consumer = consumer
                        ?? throw new ArgumentNullException(nameof(consumer));

            _pcQueue = pcQueue
                       ?? throw new ArgumentNullException(nameof(pcQueue));

            _settings = settings
                        ?? throw new ArgumentNullException(nameof(settings));

            InitializeTasks(settings.MaxNumberOfParallelTasks);
        }

        /// <inheritdoc />
        public void Add(string inputData)
        {
            _producer.Produce(inputData);
        }

        /// <inheritdoc />
        public void Stop()
        {
            _consumer.Stop();
            _pcQueue.Stop();

            WaitAllTasks();
        }

        private void WaitAllTasks()
        {
            try
            {
                Task.WaitAll(_tasks);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    _errorHandler.HandleError(e.Message);
                }
            }
        }

        private void InitializeTasks(int maxNumberOfParallelTasks)
        {
            _tasks = new Task[maxNumberOfParallelTasks];
            for (var i = 0; i < maxNumberOfParallelTasks; i++)
            {
                _tasks[i] = new Task(Consume);
                _tasks[i].Start();
            }
        }

        private void Consume()
        {
            _consumer.Consume();
        }
    }
}
