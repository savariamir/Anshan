using Microsoft.AspNetCore.Http;

namespace Anshan.Network.Abstractions
{
    public interface ICurrentHttpContext
    {
        HttpContext? HttpContext { get; set; }
    }
}