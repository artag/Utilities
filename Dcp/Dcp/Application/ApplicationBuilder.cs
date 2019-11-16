using System;
using Common;
using Microsoft.Extensions.DependencyInjection;

namespace Dcp
{
    /// <summary>
    /// Инициализатор приложения.
    /// </summary>
    internal class ApplicationBuilder
    {
        /// <summary>
        /// Инициализировать приложение.
        /// </summary>
        /// <returns>Инициализированное приложение.</returns>
        public static Result<IApplication> Build()
        {
            var provider = BuildProvider();
            if (provider.IsFailure)
            {
                return Result<IApplication>.Fail(provider.Error);
            }

            var application = provider.Value.GetService<IApplication>();

            return Result<IApplication>.Ok(application);
        }

        private static Result<IServiceProvider> BuildProvider()
        {
            var services = new ServiceCollection();

            var settings = SettingsProvider.Get();
            if (settings.IsFailure)
            {
                return Result<IServiceProvider>.Fail(settings.Error);
            }

            services.AddSingleton<Settings>(settings.Value);

            var maxNumberOfParallelTasks = settings.Value.MaxNumberOfParallelTasks;
            services.AddSingleton<IPCQueue, PCQueue>(s => new PCQueue(maxNumberOfParallelTasks));

            services.AddSingleton<IApplication, Application>();
            services.AddSingleton<IConsumer<string>, Consumer>();
            services.AddSingleton<IErrorHandler<string>, ConsoleLog>();
            services.AddSingleton<IFileHandler<Result<ulong>>, FileLettersCounter>();
            services.AddSingleton<IResultHandler<ConsumerResult>, ResultFileWriter>();
            services.AddSingleton<IProducer<string>, Producer>();

            return Result<IServiceProvider>.Ok(services.BuildServiceProvider());
        }
    }
}
