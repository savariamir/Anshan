using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anshan.Persistence.Outbox;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Anshan.OutboxProcessor.DataStore.Mongo
{
    public static class MongoQueries
    {
        public static void UpdateOutboxItems(this IMongoCollection<OutboxItem> outboxPositionCollection, List<OutboxItem> items)
        {
            items.ForEach(p => p.PublishDateTime = DateTime.UtcNow);

            foreach (var outboxItem in items)
            {
                outboxItem.PublishDateTime = DateTime.UtcNow;
                outboxPositionCollection.ReplaceOne(c => c.Id == outboxItem.Id, outboxItem);
            }
        }

        public static async Task<List<OutboxItem>> GetOutboxItems(
            this IMongoCollection<OutboxItem> outBoxCollection)
        {
            var filter = Builders<OutboxItem>.Filter.Eq("PublishDateTime", BsonNull.Value);

            return await outBoxCollection.Find(filter).Limit(100).ToListAsync();
        }
    }
}