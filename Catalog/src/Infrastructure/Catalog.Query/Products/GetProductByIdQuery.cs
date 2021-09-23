using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Mongo.Abstractions;
using Catalog.Domain.Products;
using Catalog.Query.Model.Products;
using LiteBus.Queries.Abstractions;
using MongoDB.Driver;

namespace Catalog.Query.Products
{
    public class GetProductByIdQuery : IQuery<ProductQueryModel>
    {
        public string Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductQueryModel>
    {
        private readonly IMongoConnection _mongoConnection;

        public GetProductByIdQueryHandler(IMongoConnection mongoConnection)
        {
            _mongoConnection = mongoConnection;
        }

        public async Task<ProductQueryModel> HandleAsync(GetProductByIdQuery query,
            CancellationToken cancellationToken = new())
        {
            var connection = _mongoConnection.GetCollection<Product>();

            var product = await connection.Find(FilterDefinition<Product>.Empty)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Vendor = product.Vendor,
                Price = product.Price.Value,
                Quantity = product.Quantity,
                ProductAttributes = product.ProductAttributes.Select(p=> new ProductAttributeQueryModel
                {
                    Key = p.Key,
                    Value = p.Value
                }),
            };
        }
    }
}