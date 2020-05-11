using System.Collections.Generic;

namespace Core
{
    public class Calendar
    {
        public Calendar(IEnumerable<Day> days)
        {
            Days = days;
        }

        public IEnumerable<Day> Days { get; }
    }
}
