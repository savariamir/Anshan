using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Anshan.Extensions
{
    public static class Check
    {
        public static T NotNull<T>(T value, string message)
        {
            if (value is null) throw new Exception(message);

            return value;
        }

        public static IReadOnlyList<T> NotEmpty<T>(IReadOnlyList<T>? value, string message)
        {
            NotNull(value, message);

            if (value.Count == 0) throw new Exception(message);

            return value;
        }

        public static string NotEmpty(string? value, string message)
        {
            if (value is null) throw new Exception(message);

            if (value.Trim().Length == 0) throw new Exception(message);

            return value;
        }

        public static string? NullButNotEmpty(string? value, string parameterName)
        {
            if (value is not null && value.Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }

        public static IReadOnlyList<T> HasNoNulls<T>(
            [NotNull] IReadOnlyList<T>? value, string parameterName)
            where T : class
        {
            NotNull(value, parameterName);

            if (value.Any(e => e == null))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }

        public static IReadOnlyList<string> HasNoEmptyElements([NotNull] IReadOnlyList<string>? value,
                                                               string parameterName)
        {
            NotNull(value, parameterName);

            if (value.Any(s => string.IsNullOrWhiteSpace(s)))
            {
                NotEmpty(parameterName, nameof(parameterName));

                throw new ArgumentException(parameterName);
            }

            return value;
        }
    }
}