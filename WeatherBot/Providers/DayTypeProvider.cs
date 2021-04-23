namespace WeatherBot.Providers
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using WeatherBot.Enums;

    public class DayTypeProvider
    {
        public static DayType GetDayType(DateTime date)
        {
            if (date.Date.DayOfWeek == DayOfWeek.Sunday) return DayType.Weekend;

            if (IsHoliday(date.Date.Date)) return DayType.Holiday;

            if (IsSummer(date.Date.Date)) return DayType.Vacation;

            return DayType.Weekday;
        }

        private static bool IsHoliday(DateTime mydate)
        {
            string[] holiDays =
            {
                "01.01", "02.01", "03.01", "04.01", "05.01", "06.01", "07.01", "23.02", "08.03", "01.05", "02.05",
                "09.05", "04.11"
            };

            foreach (var day in holiDays)
                if (mydate.Date.ToString("dd.MM", CultureInfo.InvariantCulture) == day)
                    return true;
            return false;
        }

        private static bool IsSummer(DateTime mydate)
        {
            string[] summerMonth = {"06", "07", "08"};

            foreach (var month in summerMonth)
                if (mydate.Date.ToString("MM", CultureInfo.InvariantCulture) == month)
                    return true;
            return false;
        }
    }
}
