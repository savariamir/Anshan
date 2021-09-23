using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Domain;
using ChainRunner;
using MongoDB.Driver;

namespace Anshan.Mongo.Internal.Handlers
{
    public class DeleteBulkHandler<TAggregateRoot> : IResponsibilityHandler<IEnumerable<TAggregateRoot>>
        where TAggregateRoot : AggregateRoot<string>
    {
        private readonly IMongoCollection<TAggregateRoot> _collection;

        public DeleteBulkHandler(IMongoCollection<TAggregateRoot> collection)
        {
            _collection = collection;
        }

        public Task HandleAsync(IEnumerable<TAggregateRoot> aggregates,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            var setDeleted = Builders<TAggregateRoot>.Update.Set(c => c.IsDeleted, true);

            var listWrites = new List<WriteModel<TAggregateRoot>>();
            foreach (var aggregate in aggregates)
            {
                var filterDefinition = Builders<TAggregateRoot>.Filter.Eq(p => p.Id, aggregate.Id);
                listWrites.Add(new UpdateManyModel<TAggregateRoot>(filterDefinition, setDeleted));
            }

            return _collection.BulkWriteAsync(listWrites, cancellationToken: cancellationToken);
        }
    }
}