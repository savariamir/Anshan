using Anshan.Application;
using Catalog.Cache.Redis.Products;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Catalog.Cache.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCacheProvider(this IServiceCollection services, IConfiguration configuration)
        {
            var conf = configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(conf);

            services.Scan(scan => scan
                .FromAssemblies(typeof(IProductRedisProvider).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICacheProvider)))
                .AsMatchingInterface()
                .WithTransientLifetime());
            
            
            return services;
        }
    }
}