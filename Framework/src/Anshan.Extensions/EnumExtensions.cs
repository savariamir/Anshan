using System;
using System.Collections.Generic;

namespace Anshan.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Coverts a given integer to the specified enum type
        /// </summary>
        /// <param name="value"> The given integer </param>
        /// <typeparam name="TEnum"> The type of enum to covert to </typeparam>
        /// <returns> The converted enum </returns>
        /// <exception cref="NotSupportedException"> In case the integer cannot be converted to the specified enum </exception>
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            if (Enum.IsDefined(typeof(TEnum), value)) return (TEnum) Enum.ToObject(typeof(TEnum), value);

            throw new
                NotSupportedException($"The entered value of '{value}' cannot be converted to '{typeof(TEnum).Name}'");
        }

        /// <summary>
        ///     Converts a list of string to specified enum type
        /// </summary>
        /// <param name="values">The list of values</param>
        /// <typeparam name="TEnum">The type of enum</typeparam>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this IEnumerable<string> values) where TEnum : struct, Enum
        {
            return (TEnum) Enum.Parse(typeof(TEnum), string.Join(", ", values));
        }

        /// <summary>
        ///     Converts an array of string to specified enum type
        /// </summary>
        /// <param name="values">The list of values</param>
        /// <typeparam name="TEnum">The type of enum</typeparam>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string[] values) where TEnum : struct, Enum
        {
            return (TEnum) Enum.Parse(typeof(TEnum), string.Join(", ", values));
        }

        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, Enum
        {
            return (TEnum) Enum.Parse(typeof(TEnum), value);
        }


        public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum input) where TEnum : struct, Enum
        {
            foreach (var value in Enum.GetValues<TEnum>())
                if (input.HasFlag(value))
                    yield return value;
        }

        public static IEnumerable<string> GetFlagNames<TEnum>(this TEnum input) where TEnum : struct, Enum
        {
            foreach (var value in Enum.GetValues<TEnum>())
                if (input.HasFlag(value))
                    yield return value.ToString();
        }
    }
}