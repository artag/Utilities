using System;

namespace Core.Operations
{
    public static class Math
    {
        public static TimeSpan MultiplyTimeSpan(TimeSpan value, double multiplier)
        {
            var multipliedTicks = (long)(value.Ticks * multiplier);
            var result = TimeSpan.FromTicks(multipliedTicks);
            return result;
        }

        public static TimeSpan RoundTimeSpanToNearestMinutes(TimeSpan input, int minutes)
        {
            var totalMinutes = (int)(input + new TimeSpan(0, minutes / 2, 0)).TotalMinutes;

            return new TimeSpan(0, totalMinutes - totalMinutes % minutes, 0);
        }
    }
}
