using System;
using System.Configuration;

namespace Web.Core
{
    public static class AppSettingHelper
    {
        public static object GetAppSetting(Type expectedType, string key, bool require)
        {
            return GetAppSetting(expectedType, key, null, require);
        }

        public static object GetAppSetting(Type expectedType, string key, object defaultvalue, bool require)
        {
            var value = ConfigurationManager.AppSettings.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                if (require) throw new Exception("AppSetting: {0} chưa được cấu hình.".FormatWith(key));
                return defaultvalue ?? (expectedType.IsValueType ? Activator.CreateInstance(expectedType) : null);
            }
            try
            {
                if (expectedType == typeof(int))
                {
                    return int.Parse(value);
                }

                if (expectedType == typeof(string))
                {
                    return value;
                }

                if (expectedType == typeof(bool))
                {
                    return bool.Parse(value);
                }

                throw new Exception("Type not supported.");
            }
            catch (Exception ex)
            {
                throw new Exception("AppSetting: Cấu hình không chính xác. {0} được xác định là kiểu {1}.".FormatWith(key, expectedType), ex);
            }
        }
    }
}
