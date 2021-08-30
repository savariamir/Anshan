using Anshan.Mongo.Abstractions;
using MongoDB.Bson.Serialization.Conventions;

namespace Anshan.Mongo.Internal.Configuration.CommonConfigurations
{
    public class ImmutableTypeConfiguration : IMongoConfiguration
    {
        public void Configure()
        {
            var conventions = new ConventionPack
            {
                new ImmutableTypeClassMapConvention()
            };

            ConventionRegistry.Register(nameof(ImmutableTypeClassMapConvention), conventions, type => true);
        }
    }
}