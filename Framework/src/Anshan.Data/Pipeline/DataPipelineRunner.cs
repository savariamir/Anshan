using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Anshan.Data.Abstractions;

namespace Anshan.Data.Pipeline
{
    internal class DataPipelineRunner<TQuery, TResult> : IDataPipelineRunner<TQuery, TResult>
    {
        private readonly List<Type> _dataProviders = new();
        private readonly TQuery _query;
        private readonly IServiceProvider _serviceProvider;

        public DataPipelineRunner(IServiceProvider serviceProvider, TQuery query)
        {
            _serviceProvider = serviceProvider;
            _query = query;
        }

        public IDataPipelineRunner<TQuery, TResult> Add<TDataProvider>()
            where TDataProvider : IDataProvider<TQuery, TResult>
        {
            _dataProviders.Add(typeof(TDataProvider));

            return this;
        }

        public async Task<TResult> ExecuteAsync()
        {
            foreach (var dataProviderType in _dataProviders)
            {
                var dataProvider = _serviceProvider.GetService(dataProviderType);

                if (dataProvider is null)
                    throw new NullReferenceException($"Date provider of type '{dataProviderType}' is not registered");

                var result = await ((IDataProvider<TQuery, TResult>) dataProvider).ProvideAsync(_query);

                if (result is not null && !result.Equals(default(TResult))) return result;
            }

            return default;
        }
    }
}