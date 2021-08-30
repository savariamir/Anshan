using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Anshan.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Materializes the given IEnumerable to list if needed, then converts it to <see cref="IReadOnlyCollection{T}" />
        /// </summary>
        /// <param name="source"> the given IEnumerable </param>
        /// <typeparam name="TSource"> the type of items </typeparam>
        /// <returns> The IReadOnlyCollection equivalent of given IEnumerable </returns>
        public static IReadOnlyCollection<TSource> AsReadOnly<TSource>(this IEnumerable<TSource> source)
        {
            return new ReadOnlyCollection<TSource>(source.Materialize());
        }

        /// <summary>
        ///     Materializes a given <see cref="IEnumerable{T}" /> to <see cref="IList{T}" />
        /// </summary>
        /// <param name="source"> the given </param>
        /// <typeparam name="TSource"> The type of items in the given <see cref="IEnumerable{T}" /> </typeparam>
        /// <returns> The materialize <see cref="IList{T}" /> </returns>
        public static List<TSource> Materialize<TSource>(this IEnumerable<TSource> source)
        {
            if (source is List<TSource> materializedList)

                // Already a list, use it as is
                return materializedList;
            return source.ToList();
        }


        public static bool IsNull<TSource>(this IEnumerable<TSource> source)
        {
            return source is null;
        }

        public static bool IsNotNull<TSource>(this IEnumerable<TSource> source)
        {
            return source is not null;
        }
    }
}