using System.Collections.Generic;
using System.Threading.Tasks;
using Anshan.Data.Pipeline;

namespace Anshan.Data.Abstractions
{
    public interface IDataPipelineRunner<TQuery, TResult>
    {
        IDataPipelineRunner<TQuery, TResult> Add<TDataProvider>()
            where TDataProvider : IDataProvider<TQuery, TResult>;

        Task<TResult> ExecuteAsync();
    }

    public interface IDataStreamPipelineRunner<TQuery, TResult>
    {
        DataStreamPipelineRunner<TQuery, TResult> Add<TDataProvider>()
            where TDataProvider : IDataStreamProvider<TQuery, TResult>;

        IAsyncEnumerable<TResult> ExecuteAsync();
    }
}