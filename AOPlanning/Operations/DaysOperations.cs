using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Core;

namespace Operations
{
    public static class DaysOperations
    {
        public static Result CheckDaySkip(IEnumerable<Day> days)
        {
            if (days == null)
                return Result.Fail($"{nameof(days)} can not be null.");

            if (!days.Any())
                return Result.Fail($"Number of days can not be zero.");

            var dates = days.Select(d => d.Date);

            var minMaxDate = FindMinMaxDate(dates);
            var minDate = minMaxDate.Item1;
            var maxDate = minMaxDate.Item2;

            var currentDate = minDate;
            while (currentDate <= maxDate)
            {
                if (!dates.Contains(currentDate))
                    return Result.Fail($"Day with {currentDate:d} date doesn't exists.");

                currentDate = currentDate.AddDays(1);
            }

            return Result.Ok();
        }

        private static (DateTime, DateTime) FindMinMaxDate(IEnumerable<DateTime> dates)
        {
            if (dates.Count() == 1)
                return (dates.First(), dates.First());

            var firstDate = DateTime.MaxValue;
            var lastDate = DateTime.MinValue;

            foreach (var date in dates)
            {
                if (date < firstDate)
                    firstDate = date;

                if (date > lastDate)
                    lastDate = date;
            }

            return (firstDate, lastDate);
        }
    }
}
