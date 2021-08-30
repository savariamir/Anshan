using System;
using System.Collections;
using Anshan.Mongo.Internal.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace Anshan.Mongo.Internal.Serialization.Providers
{
    public class EnumCollectionSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type.GetInterface(nameof(IEnumerable)) is null ||
                !type.IsGenericType ||
                !type.GetGenericArguments()[0].IsEnum)
            {
                return null;
            }

            var serializerType = typeof(EnumCollectionSerializer<>).MakeGenericType(type.GetGenericArguments()[0]);

            return (IBsonSerializer) Activator.CreateInstance(serializerType);
        }
    }
}