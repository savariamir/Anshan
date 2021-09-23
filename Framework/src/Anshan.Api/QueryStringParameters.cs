using System;

namespace Anshan.Api
{
    public class QueryStringParameters
    {
        public const int MaxPageSize = 100;
        public const int DefaultPageSize = 100;
        public const int DefaultPageNumber = 1;
        private int _pageNumber = DefaultPageNumber;
        private int _pageSize = DefaultPageSize;

        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                if (value < 1) throw new Exception("Invalid page number");

                _pageNumber = value;
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value > MaxPageSize || value < 1) throw new Exception("Invalid page size");

                _pageSize = value;
            }
        }
    }
    
    public class SearchQueryString : QueryStringParameters
    {
        public string Query { set; get; }
    }
}