using System.Reflection;
using Anshan.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Mongo.Builders
{
    public class MongoRepositoryBuilder
    {
        private readonly IServiceCollection _services;

        public MongoRepositoryBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public MongoRepositoryBuilder RegisterFrom(Assembly assembly)
        {
            _services.Scan(scan => scan.FromAssemblies(assembly)
                                       .AddClasses(classes => classes.AssignableTo(typeof(IRepository)))
                                       .AsMatchingInterface()
                                       .WithLifetime(ServiceLifetime.Transient));

            return this;
        }
    }
}