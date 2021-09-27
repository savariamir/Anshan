using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Cache.Redis.Products;
using EasyPipe;

namespace Catalog.Query.Facade.Products.Middlewares
{
    public class RedisMiddleware : IMiddleware<string, object>
    {
        private readonly IProductRedisProvider _redisProvider;

        public RedisMiddleware(IProductRedisProvider cacheMemory)
        {
            _redisProvider = cacheMemory;
        }

        public async Task<object> RunAsync(string id,
            IPipelineContext context,
            Func<Task<object>> next,
            CancellationToken cancellationToken)
        {
            var redisResponse = await _redisProvider.GetProductAsync(id);

            if (redisResponse.Hint) return redisResponse.Data;

            var mongoResponse = await next();

            await _redisProvider.SetProductAsync(mongoResponse);

            return redisResponse;
        }
    }
}