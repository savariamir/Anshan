using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Anshan.Mongo.Internal.Serialization.Serializers
{
    public class EnumCollectionSerializer<TEnum> : SerializerBase<IEnumerable<TEnum>> where TEnum : struct, Enum
    {
        public override IEnumerable<TEnum> Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            List<TEnum> enums = new List<TEnum>();

            IBsonReader reader = context.Reader;
            BsonType currentBsonType = reader.GetCurrentBsonType();

            reader.ReadStartArray();

            while (reader.ReadBsonType() != BsonType.EndOfDocument)
            {
                TEnum enumValue = Enum.Parse<TEnum>(context.Reader.ReadString());
                enums.Add(enumValue);
            }

            reader.ReadEndArray();

            return enums;
        }

        public override void Serialize(BsonSerializationContext context,
                                       BsonSerializationArgs args,
                                       IEnumerable<TEnum> values)
        {
            IBsonWriter writer = context.Writer;

            if (values is null)
                writer.WriteNull();

            writer.WriteStartArray();

            foreach (TEnum enumValue in values)
            {
                context.Writer.WriteString(enumValue.ToString());
            }

            writer.WriteEndArray();
        }
    }
}