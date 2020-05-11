using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Core
{
    public static class Parser
    {
        private static string TimeSpanParseErrorMessage = "The value {0} cannot be converted at time.";
        private static string IntParseErrorMessage = "The value {0} cannot be converted at integer number.";
        private static string DoubleParseErrorMessage = "The value {0} cannot be converted at double number.";

        public static Result<IEnumerable<TimeSpan>> ParseTimeSpan(string value)
        {
            if (value.IsNullOrEmpty())
                return Result<IEnumerable<TimeSpan>>.Ok(new [] { TimeSpan.Zero });

            var trimmedValue = value.Trim();
            var times = trimmedValue.Split('-');

            var parsedTimes = new List<TimeSpan>();

            foreach (var time in times)
            {
                var values = time.Split(':');
                var count = values.Count();
                if (count > 3 || count < 2)
                    return Result<IEnumerable<TimeSpan>>.Fail(string.Format(TimeSpanParseErrorMessage, time));

                switch (count)
                {
                    case 3:
                        var parsedDHM = ParseDaysHoursMinutes(values);
                        if (parsedDHM.IsFailure)
                            return Result<IEnumerable<TimeSpan>>.Fail(string.Format(TimeSpanParseErrorMessage, time));

                        parsedTimes.Add(parsedDHM.Value);
                        break;
                    case 2:
                        var parsedHM = ParseHoursMinutes(values);
                        if (parsedHM.IsFailure)
                            return Result<IEnumerable<TimeSpan>>.Fail(string.Format(TimeSpanParseErrorMessage, time));

                        parsedTimes.Add(parsedHM.Value); 
                        break;

                    default:
                        return Result<IEnumerable<TimeSpan>>.Fail(string.Format(TimeSpanParseErrorMessage, time));
                }
            }

            return Result<IEnumerable<TimeSpan>>.Ok(parsedTimes);
        }

        public static Result<int> ParseInt(string value)
        {
            var parsed = int.TryParse(value, out var integer);
            return parsed
                ? Result<int>.Ok(integer)
                : Result<int>.Fail(string.Format(IntParseErrorMessage, value));
        }

        public static Result<double> ParseDouble(string value)
        {
            var parsed = double.TryParse(value, out var dbl);
            return parsed
                ? Result<double>.Ok(dbl)
                : Result<double>.Fail(string.Format(DoubleParseErrorMessage, value));
        }

        private static Result<TimeSpan> ParseDaysHoursMinutes(string[] values)
        {
            var daysParsed = int.TryParse(values[0], out var days);
            if (!daysParsed)
                return Result<TimeSpan>.Fail(string.Format(TimeSpanParseErrorMessage, values[0]));

            var hoursParsed = int.TryParse(values[1], out var hours);
            if (!hoursParsed)
                return Result<TimeSpan>.Fail(string.Format(TimeSpanParseErrorMessage, values[1]));

            var minuteParsed = int.TryParse(values[2], out var minutes);
            if (!minuteParsed)
                return Result<TimeSpan>.Fail(string.Format(TimeSpanParseErrorMessage, values[2]));

            return Result<TimeSpan>.Ok(new TimeSpan(days, hours, minutes, seconds: 0));
        }

        private static Result<TimeSpan> ParseHoursMinutes(string[] values)
        {
            var hoursParsed = int.TryParse(values[0], out var hours);
            if (!hoursParsed)
                return Result<TimeSpan>.Fail(string.Format(TimeSpanParseErrorMessage, values[0]));

            var minuteParsed = int.TryParse(values[1], out var minutes);
            if (!minuteParsed)
                return Result<TimeSpan>.Fail(string.Format(TimeSpanParseErrorMessage, values[1]));

            return Result<TimeSpan>.Ok(new TimeSpan(hours, minutes, seconds: 0));
        }
    }
}
