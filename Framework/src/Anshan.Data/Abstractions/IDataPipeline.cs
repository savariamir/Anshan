namespace Anshan.Data.Abstractions
{
    public interface IDataPipeline
    {
        IDataPipelineRunner<TQuery, TResult> For<TQuery, TResult>(TQuery query);

        IDataStreamPipelineRunner<TQuery, TResult> ForStream<TQuery, TResult>(TQuery query);
    }
}