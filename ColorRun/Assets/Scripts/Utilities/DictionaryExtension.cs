using System;
using System.Collections;
using System.Collections.Generic;

namespace GAMO.Hesman.Utilities {
    public static class DictionaryExtension {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmpty<TKey, TValue>(this Dictionary<TKey, TValue> source) {
            return !( source != null && source.Count > 0 );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TKey,TValue>(this Dictionary<TKey,TValue> source) {
            return source == null || source.Count <= 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        /// <param name="acceptDuplicates"></param>
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> collection, bool acceptDuplicates = false) {
            if (collection == null) {
                throw new ArgumentNullException("Empty collection");
            } else if (collection.Count == 0) return;
            foreach (KeyValuePair<TKey, TValue> item in collection) {
                if (!acceptDuplicates && !source.ContainsKey(item.Key)) {
                    source.Add(item.Key, item.Value);
                } else source.Add(item.Key, item.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue1"></typeparam>
        /// <typeparam name="TValue2"></typeparam>
        /// <param name="source"></param>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <param name="acceptDuplicates"></param>
        public static void AddRange<TKey, TValue1, TValue2>(this Dictionary<TKey, TValue1> source, Dictionary<TKey, TValue2> collection, Func<KeyValuePair<TKey, TValue2>, KeyValuePair<TKey, TValue1>> selector, bool acceptDuplicates = false) {
            if (collection == null) {
                //throw new ArgumentNullException("empty collection");
            } else if (collection.Count == 0) return;
            foreach (KeyValuePair<TKey, TValue2> pair in collection) {
                KeyValuePair<TKey, TValue1> kvp = selector(pair);
                if (!acceptDuplicates && !source.ContainsKey(kvp.Key)) {
                    source.Add(kvp.Key, kvp.Value);
                } else source.Add(kvp.Key, kvp.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static List<TValue> ToListValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) {
            List<TValue> result = new List<TValue>();
            return result.AddRange<TValue>(dictionary.Values);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<TKey> ToListKey<TKey, TValue>(this Dictionary<TKey, TValue> source) {
            List<TKey> result = new List<TKey>();
            return result.AddRange<TKey>(source.Keys);
        }
        public static TKey FindKey<TKey, TValue>(this Dictionary<TKey, TValue> source, Predicate<TKey> match) {
            return source.ToListKey().Find(match);
        }
        public static List<TKey> FindAllKeys<TKey, TValue>(this Dictionary<TKey, TValue> source, Predicate<TKey> match) {
            return source.ToListKey().FindAll(match);
        }
        public static TValue FindValue<TKey, TValue>(this Dictionary<TKey, TValue> source, Predicate<TValue> match) {
            return source.ToListValue().Find(match);
        }
        public static List<TValue> FindAllValues<TKey, TValue>(this Dictionary<TKey, TValue> source, Predicate<TValue> match) {
            return source.ToListValue().FindAll(match);
        }
        public static bool ContainsKey<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key) {
            return source.ToListKey().Contains(key);
        }
        public static bool ContainsValue<TKey, TValue>(this Dictionary<TKey, TValue> source, TValue value) {
            return source.ToListValue().Contains(value);
        }
        public static bool ContainsKey<TKey, TValue>(this Dictionary<TKey,TValue> source, Predicate<TKey> match) {
            return source.ToListKey().Contains(match);
        }
        public static bool ContainsValue<TKey, TValue>(this Dictionary<TKey, TValue> source, Predicate<TValue> match) {
            return source.ToListValue().Contains(match);
        }
    }
}
