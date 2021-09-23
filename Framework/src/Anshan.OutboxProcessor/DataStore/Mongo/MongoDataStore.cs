using System.Linq;
using System.Threading.Tasks;
using Anshan.Mongo.Abstractions;
using Anshan.Persistence.Outbox;
using Humanizer;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Anshan.OutboxProcessor.DataStore.Mongo
{
    public class MongoDataStore : IDataStore
    {
        private readonly ILogger<OutboxWorker> _logger;
        private readonly IMongoConnection _mongoConnection;
        private readonly IMongoCollection<OutboxItem> _outBoxCollection;
        private IDataStoreChangeTracker _changeTracker;

        public MongoDataStore(ILogger<OutboxWorker> logger, IMongoConnection mongoConnection)
        {
            _logger = logger;
            _mongoConnection = mongoConnection;
            _outBoxCollection = mongoConnection.GetCollection<OutboxItem>(nameof(OutboxItem).Pluralize());
        }

        public void SetSubscriber(IDataStoreChangeTracker changeTracker)
        {
            _changeTracker = changeTracker;
        }

        public async Task SubscribeForChanges()
        {
            var transaction = await _mongoConnection.MongoClient.StartSessionAsync();

            try
            {
                var items = await _outBoxCollection.GetOutboxItems();
                if (items.Any())
                {
                    transaction.StartTransaction();
                    _logger.LogInformation($"{items.Count} Events found in outbox");
                    _changeTracker.ChangeDetected(items);
                    _outBoxCollection.UpdateOutboxItems(items);

                    await transaction.CommitTransactionAsync();
                    _logger.LogInformation($"Cursor moved to position {items.Count}");
                }
            }
            catch
            {
                await transaction.AbortTransactionAsync();
            }
        }
    }
}