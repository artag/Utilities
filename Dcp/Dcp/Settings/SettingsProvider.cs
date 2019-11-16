using System;
using System.IO;
using Common;

namespace Dcp
{
    /// <summary>
    /// Поставщик настроек приложения.
    /// </summary>
    internal class SettingsProvider
    {
        /// <summary>
        /// Получить настройки для приложения.
        /// </summary>
        /// <returns></returns>
        public static Result<Settings> Get()
        {
            var args = Environment.GetCommandLineArgs();
            var argsLength = CheckCommandLineArgsLength(args);
            if (argsLength.IsFailure)
            {
                return Result<Settings>.Fail(argsLength.Error);
            }

            var sourceDirectory = GetDirectory(args[1]);
            if (sourceDirectory.IsFailure)
            {
                return Result<Settings>.Fail(sourceDirectory.Error);
            }

            var targetDirectory = GetDirectory(args[2]);
            if (targetDirectory.IsFailure)
            {
                return Result<Settings>.Fail(targetDirectory.Error);
            }

            var settings = new Settings(sourceDirectory.Value, targetDirectory.Value);

            return Result<Settings>.Ok(settings);
        }

        private static Result CheckCommandLineArgsLength(string[] args)
        {
            return args.Length == 3
                ? Result.Ok()
                : Result.Fail("Usage: dcp.exe source_directory target_directory");
        }

        private static Result<string> GetDirectory(string arg)
        {
            return Directory.Exists(arg)
                ? Result<string>.Ok(arg)
                : Result<string>.Fail($"The directory {arg} doesn't exists.");
        }
    }
}
