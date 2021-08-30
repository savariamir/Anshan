using System.Threading.Tasks;
using Anshan.Core;
using Anshan.Mongo.Abstractions;
using MongoDB.Driver;

namespace Anshan.Mongo.Internal
{
    /// <summary>
    ///     The scoped mongo implementation of unit of work
    /// </summary>
    internal class MongoUnitOfWork : IUnitOfWork
    {
        private readonly IMongoConnection _mongoConnection;

        private IClientSessionHandle _transaction;

        public MongoUnitOfWork(IMongoConnection mongoConnection)
        {
            _mongoConnection = mongoConnection;
        }

        public async Task BeginAsync()
        {
            _transaction = await _mongoConnection.MongoClient.StartSessionAsync();
            _transaction.StartTransaction();
        }

        public Task CommitAsync()
        {
            return _transaction.CommitTransactionAsync();
        }

        public Task RollbackAsync()
        {
            return _transaction.AbortTransactionAsync();
        }
    }
}