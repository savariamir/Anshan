using System.Threading.Tasks;
using Anshan.Domain;
using Anshan.Mongo.Abstractions;
using Anshan.Persistence.Outbox;

namespace Anshan.Mongo.Internal.Handlers
{
    public class OutboxDelegate
    {
        public async Task Invoke<TAggregateRoot>(TAggregateRoot aggregateRoot, IMongoConnection mongoConnection)
            where TAggregateRoot : AggregateRoot<string>
        {
            var events = aggregateRoot.GetEvents();

            if (events is null || events.Count == 0) return;

            var itemsToAddIntoOutbox = OutboxItemFactory.CreateOutboxItem(events);

            var outBoxCollection = mongoConnection.GetCollection<OutboxItem>(nameof(OutboxItem));

            await outBoxCollection.InsertManyAsync(itemsToAddIntoOutbox);
        }
    }
}