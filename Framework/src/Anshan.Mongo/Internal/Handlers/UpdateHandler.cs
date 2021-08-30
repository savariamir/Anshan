using System.Threading;
using System.Threading.Tasks;
using Anshan.Domain;
using ChainRunner;
using MongoDB.Driver;

namespace Anshan.Mongo.Internal.Handlers
{
    public class UpdateHandler<TAggregateRoot> : IResponsibilityHandler<TAggregateRoot>
        where TAggregateRoot : AggregateRoot<string>
    {
        private readonly IMongoCollection<TAggregateRoot> _collection;

        public UpdateHandler(IMongoCollection<TAggregateRoot> collection)
        {
            _collection = collection;
        }

        public Task HandleAsync(TAggregateRoot aggregateRoot,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new CancellationToken())
        {
            return _collection.ReplaceOneAsync(c => c.Id == aggregateRoot.Id,
                                               aggregateRoot,
                                               cancellationToken: cancellationToken);
        }
    }
}