using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Anshan.Security
{
    public static class PasswordHasher
    {
        private static IEnumerable<byte> GetHash(string inputString)
        {
            using HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string ToHash(this string inputString)
        {
            var sb = new StringBuilder();
            foreach (var b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}