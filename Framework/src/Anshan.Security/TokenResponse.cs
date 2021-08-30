using System;

namespace Anshan.Security
{
    public class TokenResponse
    {
        public string Token { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}