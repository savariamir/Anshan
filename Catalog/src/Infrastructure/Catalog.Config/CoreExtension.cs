using Anshan.LiteBus;
using Anshan.Mongo.Extensions;
using Catalog.Application.Products;
using Catalog.Persistence.Mongo.Repositories;
using Catalog.Query.Products;
using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using LiteBus.Queries.Extensions.MicrosoftDependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Config
{
    public static class CoreExtension
    {
        public static IServiceCollection AddCommandBus(this IServiceCollection services)
        {
            services.AddLiteBus(builder =>
            {
                builder
                    .AddCommands(commandBuilder =>
                    {
                        commandBuilder.RegisterFrom(typeof(CreateProductHandler).Assembly)
                                      .RegisterPreHandler<BeginTransactionHook>()
                                      .RegisterPostHandler<CommitTransactionHook>();
                    });
            });

            return services;
        }

        public static IServiceCollection AddQueryBus(this IServiceCollection services)
        {
            services.AddLiteBus(builder =>
            {
                builder.AddQueries(queryBuilder =>
                {
                    queryBuilder.RegisterFrom(typeof(GetProductByIdQuery).Assembly);
                });
            });

            return services;
        }
        

        public static IServiceCollection AddMongoService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongo(opt =>
            {
                opt.Connection.ConnectionString = configuration["Mongo:ConnectionString"];
                opt.Connection.DatabaseName = configuration["Mongo:DatabaseName"];

                // opt.WithConfiguration(config => { config.RegisterFrom(typeof(ArticleConfiguration).Assembly); });
                //
                opt.WithRepositories(config => { config.RegisterFrom(typeof(ProductRepository).Assembly); });
            });

            return services;
        }

    }
}