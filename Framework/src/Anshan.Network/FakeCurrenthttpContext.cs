using Anshan.Network.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Anshan.Network
{
    public class FakeCurrenthttpContext : ICurrentHttpContext
    {
        private const long IpAddress = 16885952; //turn to 192.168.1.1

        private HttpContext _httpContext;
        
        
        public FakeCurrenthttpContext()
        {
            _httpContext = new DefaultHttpContext()
            {
                Connection =
                {
                    RemoteIpAddress = new System.Net.IPAddress(IpAddress)
                }
            };
        }
        
        public HttpContext? HttpContext
        {
            get => _httpContext;
            set => value = _httpContext;
        }
    }
}