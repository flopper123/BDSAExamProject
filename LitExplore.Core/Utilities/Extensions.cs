using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LitExplore.Core.Utilities
{
    public static class Extensions
    {
        public static int ToInt(this string str)
        {
            bool hasFound = false;
            int ret = 0;

            for (int i = 0; i < str.Length; i++)
            {

                char c = str[i];
                if (c >= '0' && c <= '9')
                {
                    ret *= 10;
                    ret += (int)(c - '0');
                    hasFound = true;
                }
                else if (hasFound)
                {
                    break;
                }
            }
            return ret;
        }

        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
            CancellationToken cancellationToken = default) // Powers 
        {
            var results = new List<T>();
            await foreach (var item in items.WithCancellation(cancellationToken)
                               .ConfigureAwait(false))
                results.Add(item);
            return results;
        }

        public static async Task<HashSet<T>> ToHashSetAsync<T>(this IAsyncEnumerable<T> items,
            CancellationToken cancellationToken = default)
        {
            var results = new HashSet<T>();
            await foreach (var item in items.WithCancellation(cancellationToken).ConfigureAwait(false))
                results.Add(item);
            return results;
        }
    }
}