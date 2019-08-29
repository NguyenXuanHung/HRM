using System;
using Web.Core.Framework.Common;

namespace Web.Core.Framework.Utils
{
    public class ConvertUtils
    {
        /// <summary>
        /// searchKey
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string GetKeyword(string keyword)
        {
            keyword = keyword.ToLower();
            char[] charArray = keyword.ToCharArray();
            keyword = "";
            foreach (char ch in charArray)
            {
                string str = ch.ToString();
                switch (ch)
                {
                    case 'a':
                        str = "[aáàạảãâấầậẩẫăắằặẳẵ]";
                        break;
                    case 'd':
                        str = "[dđ]";
                        break;
                    case 'e':
                        str = "[eéèẹẻẽêếềệểễ]";
                        break;
                    case 'i':
                        str = "[iíìịỉĩýyỳỷỹ]";
                        break;
                    case 'o':
                        str = "[oóòọỏõôốồộổỗơớờợởỡ]";
                        break;
                    case 's':
                    case 'x':
                        str = "[sx]";
                        break;
                    case 'u':
                        str = "[uúùụủũưứừựửữ]";
                        break;
                    case 'y':
                        str = "[ýyỳỷỹiíìịỉĩ]";
                        break;
                }
                keyword += str;
            }
            return keyword.Replace(" ", "%");
        }

        #region DateTime
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetStartDayOfMonth()
        {
            // start day of month
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastDayOfMonth()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            // last day of month
            return firstDayOfMonth.AddMonths(1).AddDays(-1); 
        }

        /// <summary>
        /// Convert total time from minutes to hours
        /// </summary>
        /// <param name="totalTime"></param>
        /// <returns></returns>
        public static double ConvertToHour(double totalTime)
        {
            return Math.Round(totalTime / Constant.MinutesInHour, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsDateNull(DateTime date)
        {
            return date.ToString("yyyy").Contains("0001");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsDateNull(DateTime? date)
        {
            return !date.HasValue || this.IsDateNull(date.Value);
        }

        #endregion
    }
}
