using System;
using System.Collections.Generic;
using System.Text;

namespace ICP.Candid.Utilities
{
    public static class EnumerableUtil
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
    }
}
