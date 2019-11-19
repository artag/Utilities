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

            services.AddSingleton<IConsumer, Consumer>();
            services.AddSingleton<IPCQueue<string>, PCQueue<string>>();
            services.AddSingleton<IProducer<string>, Producer>();
            services.AddSingleton<IPCController<string>, PCController>();

            services.AddSingleton<IApplication, Application>();
            services.AddSingleton<IErrorHandler<string>, ConsoleLog>();
            services.AddSingleton<IFileHandler<Result<ulong>>, FileLettersCounter>();
            services.AddSingleton<IResultHandler<ConsumerResult>, ResultFileWriter>();

            return Result<IServiceProvider>.Ok(services.BuildServiceProvider());
        }
    }
}
