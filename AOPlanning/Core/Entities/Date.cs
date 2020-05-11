using System;
using Common;

namespace Core
{
    public class Date : ValueObject<Date>
    {
        private static string ParseErrorMessage = "The value {0} cannot be converted at date.";

        public Date(DateTime date)
        {
            Value = date;
        }

        public DateTime Value { get; }

        public static Result<Date> Create(string value)
        {
            var trimmedValue = value.Trim();
            var dateParts = trimmedValue.Split('.', ',', '/', '\\');

            if (dateParts.Length != 3)
                return Result<Date>.Fail(string.Format(ParseErrorMessage, value));

            var dayParsed = int.TryParse(dateParts[0], out var day);
            if (!dayParsed)
                return Result<Date>.Fail(string.Format(ParseErrorMessage, dateParts[0]));

            var monthParsed = int.TryParse(dateParts[1], out var month);
            if (!monthParsed)
                return Result<Date>.Fail(string.Format(ParseErrorMessage, dateParts[1]));

            var yearParsed = int.TryParse(dateParts[2], out var year);
            if (!yearParsed)
                return Result<Date>.Fail(string.Format(ParseErrorMessage, dateParts[2]));

            var date = new Date(new DateTime(year, month, day));

            return Result<Date>.Ok(date);
        }

        /// <inheritdoc />
        protected override bool EqualsCore(Date other)
        {
            return Value == other.Value;
        }

        /// <inheritdoc />
        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }
    }
}
