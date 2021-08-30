using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Anshan.Mongo.Abstractions
{
    public interface IMongoConnection
    {
        public MongoClient MongoClient { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string name = default);

        GridFSBucket<TId> CreateBucket<TId>(GridFSBucketOptions options = null);
    }
}