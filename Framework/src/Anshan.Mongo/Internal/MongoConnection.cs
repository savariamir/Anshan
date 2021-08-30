using System.Collections.Generic;
using Anshan.Mongo.Abstractions;
using Anshan.Mongo.Options;
using Humanizer;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Anshan.Mongo.Internal
{
    internal class MongoConnection : IMongoConnection
    {
        private readonly Dictionary<string, object> _cachedCollections = new();
        private readonly IMongoDatabase _database;

        public MongoConnection(MongoConnectionOptions mongoConnectionOptions)
        {
            MongoClient = new MongoClient(mongoConnectionOptions.ConnectionString);

            _database = MongoClient.GetDatabase(mongoConnectionOptions.DatabaseName);
        }

        public MongoClient MongoClient { get; }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = typeof(TDocument).Name.Pluralize();
            }

            if (_cachedCollections.ContainsKey(name))
            {
                return (IMongoCollection<TDocument>) _cachedCollections[name];
            }

            var collection = _database.GetCollection<TDocument>(name);
            _cachedCollections[name] = collection;
            return collection;
        }

        public GridFSBucket<TId> CreateBucket<TId>(GridFSBucketOptions options = null)
        {
            return new(_database);
        }
    }
}