using System.Text.RegularExpressions;

namespace Anshan.Extensions
{
    public static class StringExtension
    {
        public static bool ContainsNumber(this string text)
        {
            return Regex.IsMatch(text, @"^(?=.*\d).+$");
        }
    }
}