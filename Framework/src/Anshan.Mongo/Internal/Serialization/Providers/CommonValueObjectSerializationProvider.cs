using System;
using Anshan.Domain;
using Anshan.Mongo.Internal.Serialization.Serializers;
using MongoDB.Bson.Serialization;

namespace Anshan.Mongo.Internal.Serialization.Providers
{
    public class CommonValueObjectSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type == typeof(Identification))
            {
                return new IdentificationSerializer();
            }

            if (type == typeof(Slug))
            {
                return new SlugSerializer();
            }

            if (type == typeof(Text))
            {
                return new TextSerializer();
            }

            return null;
        }
    }
}