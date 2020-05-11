using System;
using System.Collections.Generic;
using Common;

namespace Core
{
    public class TimeSlot : ValueObject<TimeSlot>
    {
        private static string ParseErrorMessage = "The value {0} cannot be converted at time.";

        private TimeSpan? _duration;

        protected TimeSlot(TimeSpan startTime, TimeSpan endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public TimeSpan StartTime { get; }

        public TimeSpan EndTime { get; }

        public TimeSpan Duration
        {
            get
            {
                if (_duration.HasValue)
                    return _duration.Value;

                _duration = EndTime - StartTime;
                return _duration.Value;
            }
        }

        public static Result<TimeSlot> Create(string value)
        {
            var parsed = Parser.ParseTimeSpan(value);
            if (parsed.IsFailure)
                return Result<TimeSlot>.Fail(parsed.Error);

            var parsedTimes = new Queue<TimeSpan>();
            foreach (var time in parsed.Value)
            {
                parsedTimes.Enqueue(time);
            }

            switch (parsedTimes.Count)
            {
                case 1:
                {
                    var time = parsedTimes.Dequeue();
                    return Result<TimeSlot>.Ok(new TimeSlot(time, time));
                }

                case 2:
                {
                    var startTime = parsedTimes.Dequeue();
                    var endTime = parsedTimes.Dequeue();
                    return Result<TimeSlot>.Ok(new TimeSlot(startTime, endTime));
                }

                default:
                    return Result<TimeSlot>.Fail(string.Format(ParseErrorMessage, value));
            }
        }

        /// <inheritdoc />
        protected override bool EqualsCore(TimeSlot other)
        {
            return StartTime == other.StartTime && EndTime == other.EndTime;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return StartTime.GetHashCode() | EndTime.GetHashCode();
        }

        public override string ToString()
        {
            return $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}, ({Duration:hh\\:mm})";
        }
    }
}
