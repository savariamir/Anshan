using System.Threading.Tasks;
using Anshan.Application;

namespace Catalog.Cache.Redis.Products
{
    public interface IProductRedisProvider : ICacheProvider
    {
        Task<CacheResponse<object>> GetProductAsync(string id);
        Task SetProductAsync(object sqlResponse);
    }
}