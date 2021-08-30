using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anshan.Data.Abstractions
{
    public interface IDataProvider
    {
    }

    /// <summary>
    ///     Provides desired data type based on given query
    /// </summary>
    /// <typeparam name="TQuery">The given query type</typeparam>
    /// <typeparam name="TResult">Teh desired data type</typeparam>
    /// <remarks>
    ///     Acts same as queries in cqrs pattern
    /// </remarks>
    public interface IDataProvider<in TQuery, TResult> : IDataProvider
    {
        Task<TResult> ProvideAsync(TQuery context);
    }

    /// <summary>
    ///     Provides desired data type based on given query in form of stream
    /// </summary>
    /// <typeparam name="TQuery">The given query type</typeparam>
    /// <typeparam name="TResult">Teh desired data type</typeparam>
    /// <remarks>
    ///     Acts same as queries in cqrs pattern
    /// </remarks>
    public interface IDataStreamProvider<in TQuery, out TResult> : IDataProvider
    {
        IAsyncEnumerable<TResult> StreamAsync(TQuery context);
    }
}