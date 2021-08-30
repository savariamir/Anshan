using System;
using System.Collections.Generic;
using Anshan.Validator;

namespace Anshan.Application.Exceptions
{
    public class CustomApplicationException : Exception
    {
        public CustomApplicationException(string message, int statusCode = 400)
        {
            Results = new[]
            {
                new ValidationResult(message, statusCode.ToString())
            };

            StatusCode = statusCode;
        }

        public CustomApplicationException(IEnumerable<ValidationResult> results, int statusCode = 400)
        {
            Results = results;
            StatusCode = statusCode;
        }

        public IEnumerable<ValidationResult> Results { get; }

        public int StatusCode { get; }
    }
}