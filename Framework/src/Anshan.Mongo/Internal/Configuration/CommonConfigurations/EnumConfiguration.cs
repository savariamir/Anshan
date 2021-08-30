using Anshan.Mongo.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Anshan.Mongo.Internal.Configuration.CommonConfigurations
{
    public class EnumConfiguration : IMongoConfiguration
    {
        public void Configure()
        {
            // Set up MongoDB conventions
            var pack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }
    }
}