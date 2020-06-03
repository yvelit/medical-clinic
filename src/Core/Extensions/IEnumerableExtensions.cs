using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<T> MergeSortDescending<T>(this IEnumerable<T> unsorted)
            where T : IComparable<T>
        {
            return unsorted.MergeSort().Reverse();
        }

        public static IEnumerable<T> MergeSort<T>(this IEnumerable<T> unsorted)
            where T : IComparable<T>
        {
            if (unsorted.IsNullOrEmpty() || unsorted.Count() <= 1)
            {
                return unsorted;
            }

            var unsortedArray = unsorted.ToArray();

            var left = new List<T>();
            var right = new List<T>();

            for (int i = 0; i < unsortedArray.Length; i++)
            {
                if (i % 2 == 0)
                {
                    left.Add(unsortedArray[i]);
                }
                else
                {
                    right.Add(unsortedArray[i]);
                }
            }

            left = left
                .AsEnumerable()
                .MergeSort()
                .ToList()
                ;

            right = right
                .AsEnumerable()
                .MergeSort()
                .ToList()
                ;

            return Merge(left, right);
        }

        private static IEnumerable<T> Merge<T>(ICollection<T> left, ICollection<T> right)
            where T : IComparable<T>
        {
            var result = new List<T>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    var comparison = left.First().CompareTo(right.First());

                    if (comparison <= 0)
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }

            return result;
        }
    }
}
