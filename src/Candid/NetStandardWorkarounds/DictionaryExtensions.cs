using System;
using System.Collections.Generic;

#if NETSTANDARD2_0
internal static class DictionaryExtensions
{
	public static TValue? GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
	{
		if (dictionary == null) { throw new ArgumentNullException(nameof(dictionary)); } // using C# 6
		if (key == null) { throw new ArgumentNullException(nameof(key)); } //  using C# 6

		TValue value;
		return dictionary.TryGetValue(key, out value) ? value : defaultValue;
	}
	public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> source, out TKey Key, out TValue Value)
	{
		Key = source.Key;
		Value = source.Value;
	}
}
#endif
