﻿using System.Collections.Generic;
using System.Diagnostics;

namespace Web.Core
{
    public static class CollectionExtension
    {
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return (collection == null) || (collection.Count == 0);
        }

        [DebuggerStepThrough]
        public static bool IsEmpty<T>(this ICollection<T> instance)
        {
            return instance.Count == 0;
        }

        [DebuggerStepThrough]
        public static void AddRange<T>(this ICollection<T> instance, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                instance.Add(item);
            }
        }
    }
}
