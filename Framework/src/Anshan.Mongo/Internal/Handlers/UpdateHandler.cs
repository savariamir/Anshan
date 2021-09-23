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
        private readonly bool _checkVersion;

        public UpdateHandler(IMongoCollection<TAggregateRoot> collection, bool checkVersion)
        {
            _collection = collection;
            _checkVersion = checkVersion;
        }

        public Task HandleAsync(TAggregateRoot aggregateRoot,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            var filter = Builders<TAggregateRoot>.Filter.Eq(a => a.Id, aggregateRoot.Id);

            if (_checkVersion)
            {
                var currentVersion = aggregateRoot.Version - 1;
                filter &= Builders<TAggregateRoot>.Filter.Eq(a => a.Version, currentVersion);
            }

            return _collection.ReplaceOneAsync(filter,
                                               aggregateRoot,
                                               cancellationToken: cancellationToken,
                                               options: new ReplaceOptions { IsUpsert = false });
        }
    }
}