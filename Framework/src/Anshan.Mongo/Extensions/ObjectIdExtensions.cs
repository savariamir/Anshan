using Anshan.Query;
using MongoDB.Bson;

namespace Anshan.Mongo.Extensions
{
    public static class ObjectIdExtensions
    {
        public static void EnsureIdIsValid(this string id)
        {
            if (!ObjectId.TryParse(id, out _)) throw new NotFoundResource();
        }
    }
}