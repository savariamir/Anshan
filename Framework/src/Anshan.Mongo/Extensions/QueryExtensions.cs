using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using MongoDB.Driver;

namespace Anshan.Mongo.Extensions
{
    public static class QueryExtensions
    {
        public static IFindFluent<TDocument, TProjection> Paginate<TDocument, TProjection>(
            this IFindFluent<TDocument, TProjection> findFluent, int pageNumber, int size)
        {
            return findFluent.Skip((pageNumber - 1) * size)
                             .Limit(size);
        }

        public static async IAsyncEnumerable<TProjection> ToAsyncEnumerable<TDocument, TProjection>(
            this IFindFluent<TDocument, TProjection> findFluent,
            [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var asyncCursor = await findFluent.ToCursorAsync(cancellationToken);

            while (await asyncCursor.MoveNextAsync(cancellationToken))
            {
                foreach (var current in asyncCursor.Current)
                {
                    yield return current;
                }
            }
        }

        public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IAsyncCursor<T> asyncCursor)
        {
            while (await asyncCursor.MoveNextAsync())
            {
                foreach (var current in asyncCursor.Current)
                {
                    yield return current;
                }
            }
        }
    }
}