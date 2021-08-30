using Anshan.Mongo.Abstractions;
using Anshan.Mongo.Internal.Serialization.Providers;
using MongoDB.Bson.Serialization;

namespace Anshan.Mongo.Internal.Configuration.CommonConfigurations
{
    public class CommonValueObjectConfiguration : IMongoConfiguration
    {
        public void Configure()
        {
            BsonSerializer.RegisterSerializationProvider(new CommonValueObjectSerializationProvider());
        }
    }
}