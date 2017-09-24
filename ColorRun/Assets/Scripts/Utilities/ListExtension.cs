using System;
using System.Collections.Generic;

namespace Subzero.Utilities
{
    public static class ListExtension
    {
        #region CONVERTER

        /// <summary>
        /// Create a list from array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<T> FromArray<T>(Array array)
        {
            List<T> result = new List<T>(array.Length);
            foreach (T item in array)
            {
                result.Add(item);
            }
            return result;
        }

        public static List<T> FromEnumerable<T>(IEnumerable<T> source)
        {
            if (source != null)
            {
                return new List<T>(source);
            }
            return new List<T>();
        }

        /// <summary>
        /// Parse type of item in list by selector condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static List<TResult> Parse<T, TResult>(this List<T> list, Func<T, TResult> selector, bool useDefault = true)
        {
            List<TResult> result = new List<TResult>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null) result.Add(selector(list[i]));
                else if (useDefault) result.Add(default(TResult));
            }
            return result;
        }

        /// <summary>
        /// Parse type of item in list by selector condition, while satisfied selectable condition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="list"></param>
        /// <param name="selector"></param>
        /// <param name="selectable"></param>
        /// <param name="useDefault"></param>
        /// <returns></returns>
        public static List<TResult> Parse<T, TResult>(this List<T> list, Func<T, TResult> selector, Predicate<T> selectable, bool useDefault = false)
        {
            List<TResult> result = new List<TResult>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null && selectable(list[i])) result.Add(selector(list[i]));
                else if (useDefault) result.Add(default(TResult));
            }
            return result;
        }

        /// <summary>
        /// Create a dictionary from list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="list"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<T, TKey, TValue>(this List<T> list, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>(list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                result.Add(keySelector(list[i]), valueSelector(list[i]));
            }
            return result;
        }

        #endregion CONVERTER

        #region INFORMATION

        /// <summary>
        /// Check whether list is empty
        /// </summary>
        /// <typeparam name="T">type of list</typeparam>
        /// <param name="list"></param>
        /// <remarks>throw exceptions if list is null</remarks>
        /// <returns>true if list have at least 1 item, otherwise false</returns>
        public static bool IsEmpty<T>(this List<T> list)
        {
            return list.Count <= 0;
        }

        /// <summary>
        /// Check whether list is null or empty, similar to string.IsNullOrEmpty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>false if list either null or empty, otherwise true</returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count <= 0;
        }

        public static T GetFirstItem<T>(this List<T> list)
        {
            return list.Count > 0 ? list[0] : default(T);
        }

        public static T GetLastItem<T>(this List<T> list)
        {
            return list.Count > 0 ? list[list.Count - 1] : default(T);
        }

        public static T GetNextToLastItem<T>(this List<T> list)
        {
            return list.Count > 1 ? list[list.Count - 2] : list.GetLastItem();
        }

        public static int GetIndex<T>(this List<T> list, Predicate<T> match)
        {
            if (list.Exists(match)) return list.FindIndex(match);
            else return -1;
        }

        public static T GetNextItem<T>(this List<T> list, T item)
        {
            if (!list.IsNullOrEmpty() && list.Contains(item))
            {
                int index = list.IndexOf(item);
                if (index < list.Count - 1)
                    return list[index + 1];
            }
            return default(T);
        }

        public static bool Contains<T>(this List<T> list, Predicate<T> match)
        {
            return list.Find(match) != null;
        }

        public static int Count<T>(this List<T> list, Predicate<T> match)
        {
            if (list.IsNullOrEmpty())
                return 0;
            List<T> result = list.FindAll(match);
            if (result != null)
            {
                return result.Count;
            }
            return 0;
        }

        /// <summary>
        /// Tìm ra các phần tử giống nhau của 2 List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="compareList"></param>
        /// <returns></returns>
        public static List<T> Intersect<T>(this List<T> list, List<T> compareList)
        {
            List<T> result = new List<T>();
            if (list.Count > compareList.Count)
            {
                foreach (T item in compareList)
                {
                    if (list.Contains(item)) result.Add(item);
                }
            }
            else
            {
                foreach (T item in list)
                {
                    if (compareList.Contains(item)) result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// Tìm ra các phần tử có ở List<T> hiện tại mà không có ở List<T> dùng để so sánh
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="compareList"></param>
        /// <returns></returns>
        public static List<T> Differences<T>(this List<T> list, List<T> compareList)
        {
            List<T> result = new List<T>(list);
            if (list.Count > compareList.Count)
            {
                foreach (T item in compareList)
                {
                    if (list.Contains(item)) result.Remove(item);
                }
            }
            else
            {
                foreach (T item in list)
                {
                    if (compareList.Contains(item)) result.Remove(item);
                }
            }
            return result;
        }

        public static List<T> Join<T>(this List<T> list, List<T> joiner)
        {
            List<T> result = new List<T>(list);
            if (!joiner.IsNullOrEmpty())
                result.AddRange(joiner);
            return result;
        }

        #endregion INFORMATION

        #region MODIFY

        public static List<T> Add<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> AddRange<T>(this List<T> list, IEnumerable<T> collection)
        {
            list.AddRange(collection);
            return list;
        }

        public static List<T> Sort<T>(this List<T> list, Comparison<T> comparison)
        {
            list.Sort(comparison);
            return list;
        }

        public static bool RemoveFromIndex<T>(this List<T> list, int index)
        {
            if (index > list.Count) return false;
            list.RemoveRange(index + 1, list.Count - index - 1);
            return true;
        }

        public static void Remove<T>(this List<T> list, List<T> targets)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (list.Contains(targets[i]))
                {
                    list.Remove(targets[i]);
                }
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            System.Random rnd = new System.Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion MODIFY
    }
}