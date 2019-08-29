using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Core
{
    public class ContextCache
    {
        public bool CacheEnable = (bool)AppSettingHelper.GetAppSetting(typeof(int), "CacheEnable", false);
        public static int CacheDuration = (int)AppSettingHelper.GetAppSetting(typeof(int), "CacheDuration", false);

        private static System.Web.Caching.Cache Cache
        {
            get { return HttpContext.Current != null ? HttpContext.Current.Cache : null; }
        }

        [DebuggerStepThrough]
        public static T Get<T>(string key)
        {
            return Cache != null ? (T)Cache.Get(key) : default(T);
        }

        [DebuggerStepThrough]
        public static void Set<T>(string key, T value)
        {
            if (value == null || Cache == null)
                return;
            Cache.Insert(key, value, null, DateTime.Now.AddSeconds(CacheDuration), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        [DebuggerStepThrough]
        public static void Remove(string key)
        {
            Cache?.Remove(key);
        }

        [DebuggerStepThrough]
        public static void RemoveRegex(string pattern)
        {
            if (Cache != null)
            {
                var cacheEnum = Cache.GetEnumerator();
                var regex = new Regex(pattern);
                while (cacheEnum.MoveNext())
                {
                    if (cacheEnum.Key != null && regex.IsMatch(cacheEnum.Key.ToString()))
                        Cache.Remove(cacheEnum.Key.ToString());
                }
            }
        }

        [DebuggerStepThrough]
        public void RemoveAll()
        {
            if (Cache != null)
            {
                var cacheEnum = Cache.GetEnumerator();
                while (cacheEnum.MoveNext())
                {
                    if (cacheEnum.Key != null)
                        Cache.Remove(cacheEnum.Key.ToString());
                }
            }
        }
    }
}
