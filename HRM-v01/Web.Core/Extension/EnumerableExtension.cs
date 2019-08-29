using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Web.Core
{
    public static class EnumerableExtension
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Random<T>(
           this IEnumerable<T> source, int count, bool allowDuplicates)
        {
            if (source == null) throw new ArgumentNullException("source");
            return RandomIterator(source, count, -1, allowDuplicates);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> Random<T>(
        this IEnumerable<T> source, int count, int seed,
           bool allowDuplicates)
        {
            if (source == null) throw new ArgumentNullException("source");
            return RandomIterator(source, count, seed,
                allowDuplicates);
        }

        [DebuggerStepThrough]
        static IEnumerable<T> RandomIterator<T>(IEnumerable<T> source,
            int count, int seed, bool allowDuplicates)
        {

            // take a copy of the current list
            var buffer = new List<T>(source);

            // create the "random" generator, time dependent or with 
            // the specified seed
            Random random;
            random = seed < 0 ? new Random() : new Random(seed);

            count = Math.Min(count, buffer.Count);

            if (count > 0)
            {
                // iterate count times and "randomly" return one of the 
                // elements
                for (int i = 1; i <= count; i++)
                {
                    // maximum index actually buffer.Count -1 because 
                    // Random.Next will only return values LESS than 
                    // specified.
                    int randomIndex = random.Next(buffer.Count);
                    yield return buffer[randomIndex];
                    if (!allowDuplicates)
                        // remove the element so it can't be selected a 
                        // second time
                        buffer.RemoveAt(randomIndex);
                }
            }
        }

        [DebuggerStepThrough]
        public static string ToDelimitedString<T>(this IEnumerable<T> source)
        {
            return source == null ? string.Empty : source.ToDelimitedString(x => x.ToString(),
                CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        [DebuggerStepThrough]
        public static string ToDelimitedString<T>(
            this IEnumerable<T> source, Func<T, string> converter)
        {
            return source == null ? string.Empty : source.ToDelimitedString(converter,
                CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        [DebuggerStepThrough]
        public static string ToDelimitedString<T>(
            this IEnumerable<T> source, string separator)
        {
            return source == null ? string.Empty : source.ToDelimitedString(x => x.ToString(), separator);
        }

        [DebuggerStepThrough]
        public static string ToDelimitedString<T>(this IEnumerable<T> source,
            Func<T, string> converter, string separator)
        {
            return string.Join(separator, source.Select(converter).ToArray());
        }

        [DebuggerStepThrough]
        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for(var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            var values = new object[props.Count];
            foreach(var item in data)
            {
                for(var i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
