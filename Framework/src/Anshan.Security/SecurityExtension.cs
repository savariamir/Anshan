using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Anshan.Security
{
    public class Hash
    {
        public static string Create(string text, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                     text,
                                                                     Encoding.ASCII.GetBytes(salt),
                                                                     KeyDerivationPrf.HMACSHA512,
                                                                     10000,
                                                                     256 / 8));

            return hashed;
        }

        public static bool Validate(string value, string salt, string hash)
        {
            return Create(value, salt) == hash;
        }
    }
}