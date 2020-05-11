using System;

namespace Core
{
    public static class Converter
    {
        public static string GetDayOfWeekShort(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "пн";
                case DayOfWeek.Tuesday:
                    return "вт";
                case DayOfWeek.Wednesday:
                    return "ср";
                case DayOfWeek.Thursday:
                    return "чт";
                case DayOfWeek.Friday:
                    return "пт";
                case DayOfWeek.Saturday:
                    return "сб";
                case DayOfWeek.Sunday:
                    return "вс";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "понедельник";
                case DayOfWeek.Tuesday:
                    return "вторник";
                case DayOfWeek.Wednesday:
                    return "среда";
                case DayOfWeek.Thursday:
                    return "четверг";
                case DayOfWeek.Friday:
                    return "пятница";
                case DayOfWeek.Saturday:
                    return "суббота";
                case DayOfWeek.Sunday:
                    return "воскресенье";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetMonthNameShort(int month)
        {
            switch (month)
            {
                case 1:
                    return "янв";
                case 2:
                    return "фев";
                case 3:
                    return "мар";
                case 4:
                    return "апр";
                case 5:
                    return "май";
                case 6:
                    return "июн";
                case 7:
                    return "июл";
                case 8:
                    return "авг";
                case 9:
                    return "сен";
                case 10:
                    return "окт";
                case 11:
                    return "ноя";
                case 12:
                    return "дек";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "январь";
                case 2:
                    return "февраль";
                case 3:
                    return "март";
                case 4:
                    return "апрель";
                case 5:
                    return "май";
                case 6:
                    return "июнь";
                case 7:
                    return "июль";
                case 8:
                    return "август";
                case 9:
                    return "сентябрь";
                case 10:
                    return "октябрь";
                case 11:
                    return "ноябрь";
                case 12:
                    return "декабрь";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
