using System.Collections.Generic;

namespace Anshan.Extensions
{
    public static class HashsetExtensions
    {
        /// <summary>
        ///     Converts a given <see cref="HashSet{T}" /> to <see cref="IReadOnlyCollection{T}" />
        /// </summary>
        /// <param name="source"> the given <see cref="HashSet{T}" /> </param>
        /// <typeparam name="TSource"> the type of items in the given <see cref="HashSet{T}" /> </typeparam>
        /// <returns> The <see cref="IReadOnlyCollection{T}" /> equivalent of the given <see cref="HashSet{T}" /> </returns>
        public static IReadOnlyCollection<TSource> AsReadOnly<TSource>(this HashSet<TSource> source)
        {
            return source;
        }
    }
}