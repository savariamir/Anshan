using System.Collections.Generic;
using System.Linq;
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
        private readonly bool _checkVersion;
        private readonly int _version;

        public DeleteHandler(IMongoCollection<TAggregateRoot> collection)
        {
            _collection = collection;
            _checkVersion = false;
        }
        
        public DeleteHandler(IMongoCollection<TAggregateRoot> collection, int version)
        {
            _collection = collection;
            _checkVersion = true;
            _version = version;
        }

        public Task HandleAsync(TAggregateRoot aggregateRoot,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            var setDeleted = Builders<TAggregateRoot>.Update.Set(c => c.IsDeleted, true);
            var filter = Builders<TAggregateRoot>.Filter.Eq(a => a.Id, aggregateRoot.Id);

            if (_checkVersion)
            {
                filter &= Builders<TAggregateRoot>.Filter.Eq(a => a.Version, _version);
            }

            return _collection.UpdateOneAsync(filter, setDeleted, cancellationToken: cancellationToken);
        }
    }
}