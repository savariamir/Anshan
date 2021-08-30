using System;
using Anshan.Data.Abstractions;

namespace Anshan.Data.Pipeline
{
    internal class DataPipeline : IDataPipeline
    {
        private readonly IServiceProvider _serviceProvider;

        public DataPipeline(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IDataPipelineRunner<TQuery, TResult> For<TQuery, TResult>(TQuery query)
        {
            return new DataPipelineRunner<TQuery, TResult>(_serviceProvider, query);
        }

        public IDataStreamPipelineRunner<TQuery, TResult> ForStream<TQuery, TResult>(TQuery query)
        {
            return new DataStreamPipelineRunner<TQuery, TResult>(_serviceProvider, query);
        }
    }
}