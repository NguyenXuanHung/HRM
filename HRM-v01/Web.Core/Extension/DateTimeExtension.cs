using System;
using System.Diagnostics;
using System.Globalization;

namespace Web.Core
{
    public static class DateTimeExtension
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        [DebuggerStepThrough]
        public static string ToVnTime(this DateTime? time)
        {
            return time != null 
                ? "{0}:{1}".FormatWith(time.Value.Hour, time.Value.Minute)
                : string.Empty;
        }

        [DebuggerStepThrough]
        public static string ToVnTime(this DateTime time)
        {
            return "{0}:{1}".FormatWith(time.Hour, time.Minute);
        }

        [DebuggerStepThrough]
        public static string ToVnDate(this DateTime time)
        {
            return time.ToString("dd/MM/yyyy");
        }

        [DebuggerStepThrough]
        public static string ToVnDateTime(this DateTime time)
        {
            return time.ToString("dd/MM/yyyy h:mm:ss tt");
        }

        [DebuggerStepThrough]
        public static string ToVnGMT(this DateTime time)
        {
            var dtf = new CultureInfo("vi-VN").DateTimeFormat;
            return "{0}, {1}, {2} GMT+7".FormatWith(dtf.GetDayName(time.DayOfWeek), time.ToString("dd/MM/yyyy")
                , time.ToString("HH:mm"));
        }

        [DebuggerStepThrough]
        public static string GetVnDayName(this DateTime time)
        {
            return new CultureInfo("vi-VN").DateTimeFormat.GetDayName(time.DayOfWeek);
        }

        public static long GetJavascriptTimestamp(this DateTime dateTime)
        {
            var span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            var time = dateTime.Subtract(span);
            return time.Ticks / 10000;
        }

        public static DateTime? ToVnTime(this string datetime, string format)
        {
            if (DateTime.TryParseExact(datetime, format, CultureInfo.CurrentCulture,
                DateTimeStyles.None, out var parseFromDate))
                return parseFromDate;
            return null;
        }

    }
}
