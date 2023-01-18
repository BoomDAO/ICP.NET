using System;
using System.Collections;
using System.Collections.Generic;

namespace EdjCase.ICP.Candid.Utilities
{
	internal static class EnumerableUtil
	{

		public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, int size)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}
			using IEnumerator<T> e = source.GetEnumerator();

			if (e.MoveNext())
			{
				List<T> chunkBuilder = new();
				while (true)
				{
					do
					{
						chunkBuilder.Add(e.Current);
					}
					while (chunkBuilder.Count < size && e.MoveNext());

					yield return chunkBuilder.ToArray();

					if (chunkBuilder.Count < size || !e.MoveNext())
					{
						yield break;
					}
					chunkBuilder.Clear();
				}
			}
		}

		public static IEnumerable<T> Select<T>(this IEnumerable source, Func<object, T> func)
		{
			IEnumerator enumerator = source.GetEnumerator();

			while (enumerator.MoveNext())
			{
				yield return func(enumerator.Current);
			}
		}
	}
}
