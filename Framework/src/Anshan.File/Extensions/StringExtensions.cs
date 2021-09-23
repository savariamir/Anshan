using System;
using System.IO;

namespace Anshan.File.Extensions
{
    public static class StringExtensions
    {
        public static bool IsFileNameWithoutExtension(this string input, out string extension)
        {
            extension = Path.GetExtension(input);

            return !string.IsNullOrEmpty(extension);
        }

        private static bool IsEqual(this string input, string secondInput)
        {
            return string.Equals(input, secondInput, StringComparison.OrdinalIgnoreCase);
        }
    }
}