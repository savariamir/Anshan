using System;
using System.Collections.Generic;
using System.Linq;

namespace Anshan.Collections.Pagination
{
    public static class Extensions
    {
        public static IReadOnlyPaginatedCollection<T> ToPaginatedList<T>(
            this IEnumerable<T> input, long count, int currentPageNumber,
            int pageSize)
        {
            return new ReadOnlyPaginatedCollection<T>(input, count, currentPageNumber, pageSize);
        }

        public static IReadOnlyPaginatedCollection<TResult> Transform<TSource, TResult>(
            this IReadOnlyPaginatedCollection<TSource> source, Func<TSource, TResult> selector)
        {
            var result = source.Source.Select(selector);

            return new ReadOnlyPaginatedCollection<TResult>(result,
                                                            source.TotalCount,
                                                            source.CurrentPage,
                                                            source.PageSize);
        }
    }
}