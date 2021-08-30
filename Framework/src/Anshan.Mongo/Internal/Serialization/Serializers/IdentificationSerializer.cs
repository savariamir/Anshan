using Anshan.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Anshan.Mongo.Internal.Serialization.Serializers
{
    public class IdentificationSerializer : SerializerBase<Identification>
    {
        public override Identification Deserialize(BsonDeserializationContext context,
                                                   BsonDeserializationArgs args)
        {
            return context.Reader.ReadString();
        }

        public override void Serialize(BsonSerializationContext context,
                                       BsonSerializationArgs args,
                                       Identification value)
        {
            context.Writer.WriteString(value.ToString());
        }
    }
}