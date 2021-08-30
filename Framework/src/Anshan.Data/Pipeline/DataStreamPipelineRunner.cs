using System;
using System.Collections.Generic;
using Anshan.Data.Abstractions;

namespace Anshan.Data.Pipeline
{
    public class DataStreamPipelineRunner<TQuery, TResult> : IDataStreamPipelineRunner<TQuery, TResult>
    {
        private readonly List<Type> _dataProviders = new();
        private readonly TQuery _query;
        private readonly IServiceProvider _serviceProvider;

        public DataStreamPipelineRunner(IServiceProvider serviceProvider, TQuery query)
        {
            _serviceProvider = serviceProvider;
            _query = query;
        }

        public DataStreamPipelineRunner<TQuery, TResult> Add<TDataProvider>()
            where TDataProvider : IDataStreamProvider<TQuery, TResult>
        {
            _dataProviders.Add(typeof(TDataProvider));

            return this;
        }

        public IAsyncEnumerable<TResult> ExecuteAsync()
        {
            foreach (var dataProviderType in _dataProviders)
            {
                var dataProvider = _serviceProvider.GetService(dataProviderType);

                if (dataProvider is null)
                    throw new NullReferenceException($"Date provider of type '{dataProviderType}' is not registered");

                var result = ((IDataStreamProvider<TQuery, TResult>) dataProvider).StreamAsync(_query);

                if (result is not null && !result.Equals(default(TResult))) return result;
            }

            return default;
        }
    }
}