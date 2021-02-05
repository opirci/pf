using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class DateWeek
    {
        public const int MinYear = 1900;
        public const int MinWeek = 1;
        public const int MaxYear = 2100;
        public const int MaxWeek = 52;

        [DataMember(Name ="year")]
        public int Year { get; set; }
        [DataMember(Name = "week")]
        public int Week { get; set; }

        public DateWeek(int year, int week)
        {
            if (year < MinYear || year > MaxYear || week < MinWeek || week > MaxWeek)
                throw new ArgumentOutOfRangeException($"Out of range when constructing DateWeek year ={year}, week ={week}");
            this.Year = year;
            this.Week = week;
        }

        public DateWeek() : this(MinYear, MinWeek)
        {

        }

        public static DateWeek Now()
        {
            return FromDate(DateTime.Today);
        }

        public static DateWeek FromDate(DateTime date)
        {
            return new DateWeek(date.Year, GetWeek(date));
        }
        static int GetWeek(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
                time = time.AddDays(3);
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public DateTime StartDate
        {
            get { return StartDateOfWeekDate(Year, Week); }
        }
        public DateTime EndDate
        {
            get { return StartDateOfWeekDate(Year, Week).AddDays(7); }
        }


        static DateTime StartDateOfWeekDate(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)CultureInfo.InvariantCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(jan1,
                CultureInfo.InvariantCulture.DateTimeFormat.CalendarWeekRule,
                CultureInfo.InvariantCulture.DateTimeFormat.FirstDayOfWeek);

            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }
    }

    [DataContract]
    public class DateWeekRange : Range<DateWeek>
    {
        public DateWeekRange(DateWeek start, DateWeek end) : base(start, end)
        {
        }

        public DateWeekRange():this (new DateWeek(), new DateWeek())
        {

        }
    }
}
