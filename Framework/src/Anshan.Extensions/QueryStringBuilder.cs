#nullable enable

using System;
using System.Collections.Specialized;
using System.Web;

namespace Anshan.Extensions
{
    public class QueryStringBuilder
    {
        private readonly NameValueCollection _queryString;

        public QueryStringBuilder()
        {
            _queryString = HttpUtility.ParseQueryString(string.Empty);
        }

        public QueryStringBuilder Add<TValue>(string key, TValue value) where TValue : notnull
        {
            _queryString.Add(key, value.ToString());

            return this;
        }

        public string Build()
        {
            return _queryString.ToString() ??
                   throw new InvalidOperationException("You should at least add one key/value");
        }
    }
}