using System.Threading;
using System.Threading.Tasks;
using Anshan.Domain;
using ChainRunner;
using MongoDB.Driver;

namespace Anshan.Mongo.Internal.Handlers
{
    public class DeleteHandler<TAggregateRoot> : IResponsibilityHandler<TAggregateRoot>
        where TAggregateRoot : AggregateRoot<string>
    {
        private readonly IMongoCollection<TAggregateRoot> _collection;

        public DeleteHandler(IMongoCollection<TAggregateRoot> collection)
        {
            _collection = collection;
        }


        public Task HandleAsync(TAggregateRoot aggregateRoot,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new CancellationToken())
        {
            var setDeleted = Builders<TAggregateRoot>.Update.Set(c => c.IsDeleted, true);

            return _collection.UpdateOneAsync(c => c.Id == aggregateRoot.Id, setDeleted, cancellationToken: cancellationToken);
        }
    }
}