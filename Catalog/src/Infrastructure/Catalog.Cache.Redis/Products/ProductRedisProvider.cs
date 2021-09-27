using System.Threading.Tasks;

namespace Catalog.Cache.Redis.Products
{
    public class ProductRedisProvider : IProductRedisProvider
    {
        public Task<CacheResponse<object>> GetProductAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task SetProductAsync(object sqlResponse)
        {
            throw new System.NotImplementedException();
        }
    }
}