using System.Collections.Generic;

namespace Anshan.Collections.Pagination
{
    public interface IReadOnlyPaginatedCollection<out T>
    {
        public IReadOnlyCollection<T> Source { get; }

        long CurrentPage { get; }

        long TotalPages { get; }

        long PageSize { get; }

        bool HasPrevious { get; }

        bool HasNext { get; }

        long TotalCount { get; }
    }
}