using System;
using System.Collections.Generic;

namespace Core
{
    public class Day : IComparable<Day>
    {
        private IEnumerable<TimeSlot> _timeSlots;

        public Day(Date date, IEnumerable<TimeSlot> timeSlots)
        {
            Date = date.Value;
            Year = Date.Year;
            Month = Converter.GetMonthName(Date.Month);
            MonthShort = Converter.GetMonthNameShort(Date.Month);
            DayOfWeek = Converter.GetDayOfWeek(Date.DayOfWeek);
            DayOfWeekShort = Converter.GetDayOfWeekShort(Date.DayOfWeek);
            Number = Date.Day;
            TimeSlots = timeSlots;
        }

        public DateTime Date { get; }

        public int Year { get; }

        public string Month { get; }

        public string MonthShort { get; set; }

        public string DayOfWeek { get; }

        public string DayOfWeekShort { get; }

        public int Number { get; }

        public IEnumerable<TimeSlot> TimeSlots
        {
            get => _timeSlots;
            set
            {
                _timeSlots = value;
                SetWorkTime();
            }
        }

        public TimeSpan WorkTime { get; private set; }

        public TimeSpan WorkTimeLeft { get; set; }

        /// <inheritdoc />
        public int CompareTo(Day other)
        {
            return Date.CompareTo(other.Date);
        }

        public override string ToString()
        {
            var workTimeString = WorkTime == TimeSpan.Zero
                ? " вых "
                : $"{WorkTime:hh}ч {WorkTime:mm}м";

            return $"{DayOfWeekShort} {Number:00} {MonthShort} ({workTimeString})";
        }

        private void SetWorkTime()
        {
            WorkTime = TimeSpan.Zero;
            foreach (var timeSlot in TimeSlots)
                WorkTime += timeSlot.Duration;

            WorkTimeLeft = WorkTime;
        }
    }
}
