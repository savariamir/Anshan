using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Cache.Memory.Products;
using Catalog.Query.ReadModel.Products;
using EasyPipe;

namespace Catalog.Query.Facade.Products.Middlewares
{
    public class MemoryMiddleware : IMiddleware<string, ProductReadModel>
    {
        private readonly IProductCacheMemoryProvider _cacheMemory;

        public MemoryMiddleware(IProductCacheMemoryProvider cacheMemory)
        {
            _cacheMemory = cacheMemory;
        }

        public async Task<ProductReadModel> RunAsync(string id,
            IPipelineContext context,
            Func<Task<ProductReadModel>> next,
            CancellationToken cancellationToken)
        {
            var memoryResponse = _cacheMemory.GetProduct(id);

            if (memoryResponse.Hint) return memoryResponse.Data;

            var redisResponse = await next();

            _cacheMemory.SetProduct(redisResponse);

            return redisResponse;
        }
    }
}