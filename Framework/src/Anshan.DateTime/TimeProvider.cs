using System;
using System.Collections.Generic;
using System.Globalization;

namespace Anshan.DateTime
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public System.DateTime Now => System.DateTime.UtcNow;

        public string ConvertToLocalDateTime(System.DateTime utcDateTime)
        {
            var localDate = ConvertToLocalDate(utcDateTime);
            var localTime = ConvertToLocalTime(utcDateTime);

            return $"{localDate} {localTime}";
        }

        public string ConvertToDateTimeFullString(System.DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.ToLocalTime();

            var persianCalendar = new PersianCalendar();

            var year = persianCalendar.GetYear(localDateTime).ToString("0000");
            var month = persianCalendar.GetMonth(localDateTime);
            var persianMonth = GetPersianMonth(month);
            var day = persianCalendar.GetDayOfMonth(localDateTime).ToString("00");

            var convertedDate = $"{day} {persianMonth} {year} ساعت {localDateTime.ToString("HH:mm")}";

            return convertedDate;
        }

        public string ConvertToLocalDate(System.DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.ToLocalTime();

            var persianCalendar = new PersianCalendar();

            var year = persianCalendar.GetYear(localDateTime).ToString("0000");
            var month = persianCalendar.GetMonth(localDateTime).ToString("00");
            var day = persianCalendar.GetDayOfMonth(localDateTime).ToString("00");

            var persianDate = $"{year}/{month}/{day}";

            return persianDate;
        }

        public string ConvertToLocalDateWithFormat(System.DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.ToLocalTime();

            var persianCalendar = new PersianCalendar();

            var month = persianCalendar.GetMonth(localDateTime);
            var persianMonth = GetPersianMonth(month);
            var day = persianCalendar.GetDayOfMonth(localDateTime);
            var persianDay = GetPersianDay(persianCalendar.GetDayOfWeek(localDateTime));

            var persianDate = $"{persianDay} {day} {persianMonth}";

            return persianDate;
        }

        public string ConvertToLocalTime(System.DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.ToLocalTime();

            var localTime = localDateTime.ToString("HH:mm:ss");

            return localTime;
        }

        public System.DateTime? ConvertToUtcDateTime(string persianDateTime)
        {
            if (string.IsNullOrEmpty(persianDateTime)) return null;

            persianDateTime = PersianToEnglish(persianDateTime);

            persianDateTime = persianDateTime.Replace("T", " ");

            var localDateTime = System.DateTime.Parse(persianDateTime, new CultureInfo("fa-IR"));
            var utcDateTime = localDateTime.ToUniversalTime();

            return utcDateTime;
        }

        public string ConvertToLocalDateTime(System.DateTime? utcDateTime)
        {
            if (!utcDateTime.HasValue) return null;

            return ConvertToLocalDateTime(utcDateTime.Value);
        }

        public string ConvertToLocalDateTimeDifferenceNow(System.DateTime utcDateTime)
        {
            var different = Now - utcDateTime;
            var totalDays = (int) different.TotalDays;
            if (totalDays == 0)
            {
                var totalHours = (int) different.TotalHours;
                if (totalHours != 0) return $"{totalHours} ساعت پیش";
                var totalMinutes = (int) different.TotalMinutes;
                if (totalMinutes != 0) return $"{totalMinutes} دقیقه پیش";
                var totalSeconds = (int) different.TotalSeconds;
                return $"{totalSeconds} ثانیه پیش";
            }

            if (totalDays < 2) return $"{totalDays} روز" + " پیش";

            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(utcDateTime);
            var yearNow = persianCalendar.GetYear(Now);
            var totalYears = yearNow - year;
            if (totalYears < 1) return ConvertToLocalDateYearMonth(utcDateTime);
            if (totalYears >= 1) return ConvertToLocalFullDate(utcDateTime);

            return ConvertToLocalDate(utcDateTime);
        }

        public string ConvertToLocalDateYearMonth(System.DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.ToLocalTime();

            var persianCalendar = new PersianCalendar();

            var month = persianCalendar.GetMonth(localDateTime);
            var persianMonth = GetPersianMonth(month);
            var day = persianCalendar.GetDayOfMonth(localDateTime);

            var persianDate = $" {day} {persianMonth}";

            return persianDate;
        }

        public string ConvertToLocalFullDate(System.DateTime utcDateTime)
        {
            var localDateTime = utcDateTime.ToLocalTime();

            var persianCalendar = new PersianCalendar();
            var year = persianCalendar.GetYear(localDateTime).ToString("0000");
            var month = persianCalendar.GetMonth(localDateTime);
            var persianMonth = GetPersianMonth(month);
            var day = persianCalendar.GetDayOfMonth(localDateTime);

            var persianDate = $" {day} {persianMonth} {year}";

            return persianDate;
        }

        public string TimeAgo(System.DateTime dateTime)
        {
            var result = string.Empty;
            var timeSpan = System.DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
                result = string.Format("{0} ثانیه قبل", timeSpan.Seconds);
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                result = timeSpan.Minutes > 1 ? string.Format("{0} دقیقه قبل", timeSpan.Minutes) : "حدود یک دقیقه قبل";
            else if (timeSpan <= TimeSpan.FromHours(24))
                result = timeSpan.Hours > 1 ? string.Format("{0} ساعت قبل", timeSpan.Hours) : "حدود یک ساعت قبل";
            else if (timeSpan <= TimeSpan.FromDays(30))
                result = timeSpan.Days > 1 ? string.Format("{0} روز قبل", timeSpan.Days) : "دیروز";
            else if (timeSpan <= TimeSpan.FromDays(365))
                result = timeSpan.Days > 30 ? string.Format("{0} ماه قبل", timeSpan.Days / 30) : "حدود یک ماه قبل";
            else
                result = timeSpan.Days > 365 ? string.Format("{0} سال قبل", timeSpan.Days / 365) : "حدود یک سال قبل";

            return result;
        }

        #region PrivateMethods

        private string GetPersianMonth(int month)
        {
            var months = new List<string>(new[]
            {
                "فروردين", "اردیبهشت", "خرداد",
                "تیر", "مرداد", "شهریور",
                "مهر", "آبان", "آذر",
                "دی", "بهمن", "اسفند"
            });
            return months[month - 1];
        }

        private string GetPersianDay(DayOfWeek day)
        {
            var Day = new List<string>(new[]
            {
                "یکشنبه", "دوشنبه", "سه شنبه",
                "چهار شنبه", "پنج شنبه", "جمعه",
                "شنبه"
            });
            return Day[(int) day];
        }

        public static string PersianToEnglish(string input)
        {
            var lettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };
            foreach (var item in input)
                if (lettersDictionary.TryGetValue(item, out var englishDigit))
                    input = input.Replace(item, englishDigit);
            return input;
        }

        #endregion PrivateMethods
    }
}