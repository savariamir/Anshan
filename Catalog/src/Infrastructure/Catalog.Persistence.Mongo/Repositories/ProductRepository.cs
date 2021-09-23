using Anshan.Mongo;
using Anshan.Mongo.Abstractions;
using Catalog.Domain.Products;

namespace Catalog.Persistence.Mongo.Repositories
{
    public class ProductRepository : MongoRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IMongoConnection mongoConnection, string collectionName = default) : base(
            mongoConnection, collectionName)
        {
        }
    }
}