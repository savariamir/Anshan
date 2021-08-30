using System;
using Anshan.Domain;
using Anshan.Mongo.Abstractions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Anshan.Mongo.Internal.Configuration.CommonConfigurations
{
    public class EntityConfiguration : IMongoConfiguration
    {
        public void Configure()
        {
            BsonClassMap.RegisterClassMap<Entity<string>>(cm =>
            {
                cm.SetIsRootClass(true);

                cm.MapIdProperty(c => c.Id)
                  .SetIdGenerator(StringObjectIdGenerator.Instance)
                  .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.AutoMap();
            });

            BsonSerializer.RegisterSerializer(typeof(DateTime),
                                              new DateTimeSerializer(DateTimeKind.Utc, BsonType.Document));
        }
    }
}