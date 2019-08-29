using System;

namespace Web.Core.Helper
{
    public static class DatetimeHelper
    {
        /// <summary>
        /// Chuyển ngày âm sang ngày dương
        /// </summary>
        /// <param name="lunarDay"></param>
        /// <param name="lunarMonth"></param>
        /// <param name="lunarYear"></param>
        /// <param name="lunarLeap"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static int[] ConvertLunar2Solar(int lunarDay, int lunarMonth, int lunarYear, int lunarLeap, int timeZone)
        {
            long a11, b11;
            if(lunarMonth < 11)
            {
                a11 = GetLunarMonth11(lunarYear - 1, timeZone);
                b11 = GetLunarMonth11(lunarYear, timeZone);
            }
            else
            {
                a11 = GetLunarMonth11(lunarYear, timeZone);
                b11 = GetLunarMonth11(lunarYear + 1, timeZone);
            }
            long off = lunarMonth - 11;
            if(off < 0)
            {
                off += 12;
            }

            if(b11 - a11 > 365)
            {
                long leapOff = GetLeapMonthOffset(a11, timeZone);
                var leapMonth = leapOff - 2;
                if(leapMonth < 0)
                {
                    leapMonth += 12;
                }

                if(lunarLeap != 0 && lunarMonth != leapMonth)
                {
                    return new int[] { 0, 0, 0 };
                }
                else if(lunarLeap != 0 || off >= leapOff)
                {
                    off += 1;
                }
            }
            var k = Int(0.5 + (a11 - 2415021.076998695) / 29.530588853);
            var monthStart = GetNewMoonDay(k + off, timeZone);
            return JdToDate(monthStart + lunarDay - 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long Int(double d)
        {
            return (long)Math.Floor(d);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dd"></param>
        /// <param name="mm"></param>
        /// <param name="yy"></param>
        /// <returns></returns>
        public static long JdFromDate(int dd, int mm, int yy)
        {
            var a = Int((14 - mm) / 12);
            long y = yy + 4800 - a;
            long m = mm + 12 * a - 3;
            long jd = dd + Int((153 * m + 2) / 5) + 365 * y + Int(y / 4) - Int(y / 100) + Int(y / 400) - 32045;
            if(jd < 2299161)
            {
                jd = dd + Int((153 * m + 2) / 5) + 365 * y + Int(y / 4) - 32083;
            }
            return jd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>
        public static int[] JdToDate(long jd)
        {
            long a, b, c, d, e, m;
            if(jd > 2299160)
            { // After 5/10/1582, Gregorian calendar
                a = jd + 32044;
                b = Int((4 * a + 3) / 146097);
                c = a - Int((b * 146097) / 4);
            }
            else
            {
                b = 0;
                c = jd + 32082;
            }
            d = Int((4 * c + 3) / 1461);
            e = c - Int((1461 * d) / 4);
            m = Int((5 * e + 2) / 153);
            var day = e - Int((153 * m + 2) / 5) + 1;
            var month = m + 3 - 12 * Int(m / 10);
            var year = b * 100 + d - 4800 + Int(m / 10);

            return new int[] { (int)day, (int)month, (int)year };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="k"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static long GetNewMoonDay(long k, long timeZone)
        {
            double T = k / 1236.85; // Time in Julian centuries from 1900 January 0.5
            double t2 = T * T;
            double t3 = t2 * T;
            const double dr = Math.PI / 180;
            double jd1 = 2415020.75933 + 29.53058868 * k + 0.0001178 * t2 - 0.000000155 * t3;
            jd1 = jd1 + 0.00033 * Math.Sin((166.56 + 132.87 * T - 0.009173 * t2) * dr); // Mean new moon
            double m = 359.2242 + 29.10535608 * k - 0.0000333 * t2 - 0.00000347 * t3; // Sun's mean anomaly
            double mpr = 306.0253 + 385.81691806 * k + 0.0107306 * t2 + 0.00001236 * t3; // Moon's mean anomaly
            double f = 21.2964 + 390.67050646 * k - 0.0016528 * t2 - 0.00000239 * t3; // Moon's argument of latitude
            double c1 = (0.1734 - 0.000393 * T) * Math.Sin(m * dr) + 0.0021 * Math.Sin(2 * dr * m);
            c1 = c1 - 0.4068 * Math.Sin(mpr * dr) + 0.0161 * Math.Sin(dr * 2 * mpr);
            c1 = c1 - 0.0004 * Math.Sin(dr * 3 * mpr);
            c1 = c1 + 0.0104 * Math.Sin(dr * 2 * f) - 0.0051 * Math.Sin(dr * (m + mpr));
            c1 = c1 - 0.0074 * Math.Sin(dr * (m - mpr)) + 0.0004 * Math.Sin(dr * (2 * f + m));
            c1 = c1 - 0.0004 * Math.Sin(dr * (2 * f - m)) - 0.0006 * Math.Sin(dr * (2 * f + mpr));
            c1 = c1 + 0.0010 * Math.Sin(dr * (2 * f - mpr)) + 0.0005 * Math.Sin(dr * (2 * mpr + m));
            double deltat = 0;
            if(T < -11)
            {
                deltat = 0.001 + 0.000839 * T + 0.0002261 * t2 - 0.00000845 * t3 - 0.000000081 * T * t3;
            }
            else
            {
                deltat = -0.000278 + 0.000265 * T + 0.000262 * t2;
            };
            double JdNew = jd1 + c1 - deltat;
            return Int(JdNew + 0.5 + (double)timeZone / 24);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jdn"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static long GetSunLongitude(long jdn, int timeZone)
        {
            var T = (jdn - 2451545.5 - timeZone / 24) / 36525; // Time in Julian centuries from 2000-01-01 12:00:00 GMT
            var t2 = T * T;
            const double dr = Math.PI / 180; // degree to radian
            var m = 357.52910 + 35999.05030 * T - 0.0001559 * t2 - 0.00000048 * T * t2; // mean anomaly, degree
            double l0 = 280.46645 + 36000.76983 * T + 0.0003032 * t2; // mean longitude, degree
            double dl = (1.914600 - 0.004817 * T - 0.000014 * t2) * Math.Sin(dr * m);
            dl = dl + (0.019993 - 0.000101 * T) * Math.Sin(dr * 2 * m) + 0.000290 * Math.Sin(dr * 3 * m);
            double l = l0 + dl; // true longitude, degree
            // obtain apparent longitude by correcting for nutation and aberration
            double omega = 125.04 - 1934.136 * T;
            l = l - 0.00569 - 0.00478 * Math.Sin(omega * dr);
            l = l * dr;
            l = l - Math.PI * 2 * (Int(l / (Math.PI * 2))); // Normalize to (0, 2*PI)
            return Int(l / Math.PI * 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="yy"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static long GetLunarMonth11(int yy, int timeZone)
        {
            long off = JdFromDate(31, 12, yy) - 2415021;
            long k = Int(off / 29.530588853);
            long nm = GetNewMoonDay(k, timeZone);
            long sunLong = GetSunLongitude(nm, timeZone); // sun longitude at local midnight
            if(sunLong >= 9)
            {
                nm = GetNewMoonDay(k - 1, timeZone);
            }
            return nm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a11"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static int GetLeapMonthOffset(long a11, int timeZone)
        {
            var k = Int((a11 - 2415021.076998695) / 29.530588853 + 0.5);
            long last;
            var i = 1; // We start with the month following lunar month 11
            var arc = GetSunLongitude(GetNewMoonDay(k + i, timeZone), timeZone);
            do
            {
                last = arc;
                i = i + 1;
                arc = GetSunLongitude(GetNewMoonDay(k + i, timeZone), timeZone);
            } while(arc != last && i < 14);
            return i - 1;
        }

        public static bool IsNull(DateTime? datetime)
        {
            return datetime == null || datetime.Value.ToString("yyyy").Contains("0001");
        }
    }
}
