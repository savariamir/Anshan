using System;
using Anshan.Core;
using Anshan.Mongo.Abstractions;
using Anshan.Mongo.Builders;
using Anshan.Mongo.Internal;
using Anshan.Mongo.Internal.Configuration;
using Anshan.Mongo.Internal.Configuration.CommonConfigurations;
using Anshan.Mongo.Options;
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Anshan.Mongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services,
                                                  Action<MongoBuilder> config)
        {
            var repositoryBuilder = new MongoRepositoryBuilder(services);
            var connectionOptions = new MongoConnectionOptions();
            var configurationController = ConfigurationController.Instance;
            
            var builder = new MongoBuilder(connectionOptions, configurationController, repositoryBuilder);
            
            config(builder);
            
            services.AddSingleton<IMongoConnection>(_ => new MongoConnection(builder.Connection));
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();

            configurationController.Register<EntityConfiguration>();
            configurationController.Register<EnumConfiguration>();
            configurationController.Register<EnumCollectionConfiguration>();
            configurationController.Register<ImmutableTypeConfiguration>();

            configurationController.Apply();

            return services;
        }
    }
}