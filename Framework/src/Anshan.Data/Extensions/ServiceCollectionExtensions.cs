using System.Linq;
using System.Reflection;
using Anshan.Data.Abstractions;
using Anshan.Data.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataPipeline(this IServiceCollection serviceCollection,
                                                         params Assembly[] assemblies)
        {
            var dataProviders = assemblies.SelectMany(a => a.DefinedTypes)
                                          .Where(t => t.IsAssignableTo(typeof(IDataProvider)));

            foreach (var dataProvider in dataProviders) serviceCollection.AddTransient(dataProvider);

            serviceCollection.AddTransient<IDataPipeline, DataPipeline>();

            return serviceCollection;
        }
    }
}