using System.Linq;
using System.Reflection;
using Anshan.Mapper.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Mapper.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddTransient<IConditionalMapper, ConditionalMapper>();

            var types = assemblies.SelectMany(a => a.DefinedTypes);

            foreach (var typeInfo in types)
            {
                foreach (var @interface in typeInfo.ImplementedInterfaces)
                {
                    if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IMapper<,>))
                    {
                        services.AddTransient(@interface, typeInfo);
                        services.AddTransient(typeInfo);
                    }
                }
            }

            return services;
        }
    }
}