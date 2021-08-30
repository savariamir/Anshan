using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChainRunner;
using MongoDB.Driver;

namespace Anshan.Mongo.Internal.Handlers
{
    public class CreateHandler<TAggregateRoot> : IResponsibilityHandler<TAggregateRoot>,
                                                 IResponsibilityHandler<IEnumerable<TAggregateRoot>>
    {
        private readonly IMongoCollection<TAggregateRoot> _collection;

        public CreateHandler(IMongoCollection<TAggregateRoot> collection)
        {
            _collection = collection;
        }

        public Task HandleAsync(TAggregateRoot aggregateRoot, IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            return _collection.InsertOneAsync(aggregateRoot, cancellationToken: cancellationToken);
        }

        public Task HandleAsync(IEnumerable<TAggregateRoot> request, IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            return _collection.InsertManyAsync(request, cancellationToken: cancellationToken);
        }
    }
}