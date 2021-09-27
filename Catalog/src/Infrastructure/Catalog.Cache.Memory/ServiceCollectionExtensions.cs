using Anshan.Application;
using Catalog.Cache.Memory.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Cache.Memory
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMemoryCacheProvider(this  IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(typeof(ProductCacheMemoryProvider).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICacheProvider)))
                .AsMatchingInterface()
                .WithTransientLifetime());
            
            services.AddMemoryCache();
            
            return services;
        }
    }
}