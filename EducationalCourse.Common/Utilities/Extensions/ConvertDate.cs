using System.Globalization;

namespace EducationalCourse.Common.Extensions
{
    public static class PersianDate
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(value) + "/" +
                   pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }

        public static DateTime ToMiladi(this string datTime)
        {
            PersianCalendar pc = new PersianCalendar();

            var splitDate = datTime.Split("/");

            var year = Convert.ToInt32(splitDate[0]);
            var month = Convert.ToInt32(splitDate[1]);
            var day = Convert.ToInt32(splitDate[2]);

            DateTime date = new DateTime(year, month, day, pc);

            return DateTime.Parse(date.ToString(CultureInfo.CreateSpecificCulture("en-US"))).Date;
        }
    }
}
