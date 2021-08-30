using System;
using System.Security.Cryptography;

namespace Anshan.Security
{
    public class Salt
    {
        public static string Create()
        {
            var randomBytes = new byte[128 / 8];
            using var generator = RandomNumberGenerator.Create();
            generator.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}