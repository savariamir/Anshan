using System.Reflection;
using Anshan.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.MicrosoftDI.Extensions
{
    public static class FrameworkExtension
    {
        /// <summary>
        ///     Scans given <see cref="assemblies" /> and registers all implementations of <see cref="IService" /> with
        ///     scoped lifetime
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan => scan
                                  .FromAssemblies(assemblies)
                                  .AddClasses(classes => classes.AssignableTo(typeof(IService)))
                                  .AsMatchingInterface()
                                  .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddCustomServices<T>(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan => scan
                                  .FromAssemblies(assemblies)
                                  .AddClasses(classes => classes.AssignableTo(typeof(T)))
                                  .AsMatchingInterface()
                                  .WithScopedLifetime());

            return services;
        }
    }
}