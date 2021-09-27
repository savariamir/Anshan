using System;
using Catalog.Cache.Products;
using Catalog.Query.ReadModel.Products;
using Microsoft.Extensions.Caching.Memory;

namespace Catalog.Cache.Memory.Products
{
    public class ProductCacheMemoryProvider : IProductCacheMemoryProvider
    {
        private readonly IMemoryCache _cache;

        public ProductCacheMemoryProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public CacheResponse<ProductReadModel> GetProduct(string id)
        {
            var product = _cache.Get<CacheResponse<ProductReadModel>>(ProductCacheKey.GetProduct(id));
            return product;
        }

        public void SetProduct(ProductReadModel product)
        {
            _cache.Set(ProductCacheKey.GetProduct(product.Id), product,
                TimeSpan.FromSeconds(60));
        }
    }
}