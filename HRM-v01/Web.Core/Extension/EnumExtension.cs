using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Web.Core
{
    public static class EnumExtension
    {
        [AttributeUsage(AttributeTargets.All)]
        public class CustomDescriptionAttribute : DescriptionAttribute
        {
            public CustomDescriptionAttribute(string description, string value)
            {
                Description = description;
                Value = value;
            }

            public string Description { get; set; }
            public string Value { get; set; }

        }
        /// <summary>
        /// Get description of enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Description(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
        /// <summary>
        /// Gets a list of key/value pairs for an enum, using the description attribute as value
        /// </summary>
        /// <param name="enumType"/>>typeof(your enum type)
        /// <returns>A list of KeyValuePairs with enum values and descriptions</returns>
        public static List<KeyValuePair<string, string>> GetValuesAndDescription(this Type enumType)
        {
            return (from Enum enumValue in Enum.GetValues(enumType)
                    select new KeyValuePair<string, string>(enumValue.ToString(), Description(enumValue))).ToList();
        }

        /// <summary>
        /// Gets a list of key/value pairs for an enum, using the description attribute as value
        /// </summary>
        /// <param name="enumType"/>>typeof(your enum type)
        /// <returns>A list of KeyValuePairs with enum values and descriptions</returns>
        public static List<KeyValuePair<int, string>> GetIntAndDescription(this Type enumType)
        {
            return (from Enum enumValue in Enum.GetValues(enumType)
                    select new KeyValuePair<int, string>(Convert.ToInt32(enumValue), Description(enumValue))).ToList();
        }
    }
}
