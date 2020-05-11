using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Common;
using Core;

namespace AOPlan
{
    internal static class ConfigFile
    {
        public static Result<double> CheckInputArguments(string[] args)
        {
            var argsCount = args.Count();
            if (argsCount < 2 || argsCount > 3)
                return Result<double>.Fail(
                    "Usage: aoplan.exe days_configs.txt tasks_configs.txt 1.5\n" +
                    "1.5 - multiplier for tasks durations. Value by default = 1.0");

            for (int i = 0; i < 2; i++)
            {
                if (!File.Exists(args[i]))
                    return Result<double>.Fail($"File {args[i]} doesn't found");
            }

            const double multiplierByDefault = 1.0;

            if (argsCount != 3)
                return Result<double>.Ok(multiplierByDefault);

            var parsedMultiplier = Parser.ParseDouble(args[2]);
            if (parsedMultiplier.IsFailure)
                return Result<double>.Fail(parsedMultiplier.Error);

            return Result<double>.Ok(parsedMultiplier.Value);
        }

        public static Result<IEnumerable<string>> CheckLinksToJsonFiles(string configFile)
        {
            var jsonConfigFiles = File.ReadAllLines(configFile);
            if (jsonConfigFiles.Length < 0)
                return Result<IEnumerable<string>>.Fail($"File {configFile} doesn't contains configuration files");

            foreach (var jsonConfigFile in jsonConfigFiles)
            {
                if (!File.Exists(jsonConfigFile))
                    return Result<IEnumerable<string>>.Fail($"File {jsonConfigFile} doesn't found");
            }

            return Result<IEnumerable<string>>.Ok(jsonConfigFiles);
        }

        public static Result<IEnumerable<JsonDaysData>> LoadDaysData(IEnumerable<string> jsonConfigFiles)
        {
            var days = new List<JsonDaysData>();

            foreach (var jsonConfigFile in jsonConfigFiles)
            {
                var readJson = ReadJsonFromFile(jsonConfigFile);
                if (readJson.IsFailure)
                    return Result<IEnumerable<JsonDaysData>>.Fail(readJson.Error);

                var jsonFromFile = readJson.Value;

                try
                {
                    var jsonDaysData = JsonSerializer.Deserialize<JsonDaysData>(jsonFromFile);
                    days.Add(jsonDaysData);
                }
                catch (Exception ex)
                {
                    var message = $"The error occurred while reading file {jsonConfigFile}.\n" +
                                  ex.Message;

                    return Result<IEnumerable<JsonDaysData>>.Fail(message);
                }
            }

            return Result<IEnumerable<JsonDaysData>>.Ok(days);
        }

        public static Result<IEnumerable<JsonJobsData>> LoadJobsData(IEnumerable<string> jsonConfigFiles)
        {
            var jobs = new List<JsonJobsData>();

            foreach (var jsonConfigFile in jsonConfigFiles)
            {
                var readJson = ReadJsonFromFile(jsonConfigFile);
                if (readJson.IsFailure)
                    return Result<IEnumerable<JsonJobsData>>.Fail(readJson.Error);

                var jsonFromFile = readJson.Value;

                try
                {
                    var jsonJobsData = JsonSerializer.Deserialize<JsonJobsData>(jsonFromFile);
                    jobs.Add(jsonJobsData);
                }
                catch (Exception ex)
                {
                    var message = $"The error occurred while reading file {jsonConfigFile}.\n" +
                                  ex.Message;

                    return Result<IEnumerable<JsonJobsData>>.Fail(message);
                }
            }

            return Result<IEnumerable<JsonJobsData>>.Ok(jobs);
        }

        private static Result<string> ReadJsonFromFile(string jsonConfigFile)
        {
            try
            {
                var jsonFromFile = File.ReadAllText(jsonConfigFile);
                return Result<string>.Ok(jsonFromFile);
            }
            catch (Exception ex)
            {
                return Result<string>.Fail(ex.Message);
            }
        }
    }
}
