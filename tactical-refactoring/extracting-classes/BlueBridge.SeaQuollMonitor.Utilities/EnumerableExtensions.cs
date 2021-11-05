using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueBridge.SeaQuollMonitor.Utilities
{
    public static class EnumerableExtensions
    {
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<T, TKey, TValue>(
            this IEnumerable<T> enumerable,
            Func<T, TKey> keySelector,
            Func<T, TValue> valueSelector,
            IComparer<TKey>? comparer = null)
            where TKey : notnull
        {
            var result = new SortedDictionary<TKey, TValue>(comparer);
            foreach (var item in enumerable)
            {
                result.Add(keySelector(item), valueSelector(item));
            }

            return result;
        }

        public static IEnumerable<T> Buffered<T>(this IEnumerable<T> enumerable) =>
            enumerable as IReadOnlyCollection<T> ?? enumerable.ToList();
    }
}
