using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Mongo.Abstractions;
using Catalog.Domain.Products;
using Catalog.Query.ReadModel.Products;
using EasyPipe;
using MongoDB.Driver;

namespace Catalog.Query.Facade.Products.Middlewares
{
    public class MongoMiddleware : IMiddleware<string, ProductReadModel>
    {
        private readonly IMongoConnection _mongoConnection;

        public MongoMiddleware(IMongoConnection mongoConnection)
        {
            _mongoConnection = mongoConnection;
        }

        public async Task<ProductReadModel> RunAsync(string request, 
            IPipelineContext context, 
            Func<Task<ProductReadModel>> next, 
            CancellationToken cancellationToken)
        {
            var connection = _mongoConnection.GetCollection<Product>();

            var product = await connection.Find(FilterDefinition<Product>.Empty)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return new ProductReadModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price.Value,
                Quantity = product.Quantity,
                ProductAttributes = product.ProductAttributes.Select(p=> new ProductAttributeReadModel
                {
                    Key = p.Key,
                    Value = p.Value
                }),
            };
        }
    }
}