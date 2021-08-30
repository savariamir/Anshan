using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Anshan.Collections.Pagination
{
    public class ReadOnlyPaginatedCollection<T> : IReadOnlyPaginatedCollection<T>
    {
        private readonly ReadOnlyCollection<T> _readOnlyCollection;

        public ReadOnlyPaginatedCollection(IEnumerable<T> items, long totalCount, long currentPageNumber, long pageSize)
        {
            _readOnlyCollection = new ReadOnlyCollection<T>(items.ToList());
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = currentPageNumber;
            TotalPages = (long) Math.Ceiling(totalCount / (double) pageSize);
        }

        public IReadOnlyCollection<T> Source => _readOnlyCollection;

        public long CurrentPage { get; }

        public long TotalPages { get; }

        public long PageSize { get; }

        public long TotalCount { get; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;
    }
}