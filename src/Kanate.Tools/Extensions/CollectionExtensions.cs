using System.Collections.Generic;

namespace Kanate.Tools.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>(this List<T> collection) => collection.IsEmpty<T, List<T>>();
        public static bool IsEmpty<T>(this T[] collection) => collection.IsEmpty<T, T[]>();
        public static bool IsEmpty<T>(this HashSet<T> collection) => collection.IsEmpty<T, HashSet<T>>();
        public static bool IsEmpty<TKey, TValue>(this Dictionary<TKey, TValue> collection) => collection.IsEmpty<KeyValuePair<TKey, TValue>, Dictionary<TKey, TValue>>();

        public static bool IsEmpty<T, TCollection>(this TCollection collection)
            where TCollection : ICollection<T>
        {
            return EqualityComparer<TCollection>.Default.Equals(collection, default(TCollection))
                || collection.Count == 0;
        }

        public static void Upsert<TKey, TValue, TCollection>(this IDictionary<TKey, TCollection> dictionary, TKey key, TValue element)
            where TCollection : ICollection<TValue>, new()
        {
            if (dictionary != null && !EqualityComparer<TKey>.Default.Equals(key, default(TKey)))
            {
                if (dictionary.TryGetValue(key, out TCollection existingElements))
                {
                    existingElements.Add(element);
                }
                else
                {
                    dictionary[key] = new TCollection { element };
                }
            }
        }
    }
}
