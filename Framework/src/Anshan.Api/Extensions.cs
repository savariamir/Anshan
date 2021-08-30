using System.Collections.Generic;

namespace Anshan.Api
{
    public static class Extensions
    {
        public static Dictionary<string, string> ToParametersDictionary(this QueryStringParameters parameters)
        {
            return new()
            {
                {nameof(QueryStringParameters.PageNumber), parameters.PageNumber.ToString()},
                {nameof(QueryStringParameters.PageSize), parameters.PageSize.ToString()}
            };
        }

        public static bool IsDefault(this QueryStringParameters parameters)
        {
            return parameters.PageNumber == QueryStringParameters.DefaultPageNumber &&
                   parameters.PageSize == QueryStringParameters.DefaultPageSize;
        }
    }
}