using Anshan.Application;
using Catalog.Query.ReadModel.Products;

namespace Catalog.Cache.Memory.Products
{
    public interface IProductCacheMemoryProvider: ICacheProvider
    {
        CacheResponse<ProductReadModel> GetProduct(string id);
        void SetProduct(ProductReadModel redisResponse);
    }
}