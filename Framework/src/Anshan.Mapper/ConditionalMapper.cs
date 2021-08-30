using System;
using System.Collections.Generic;
using System.Linq;
using Anshan.Mapper.Abstractions;
using Anshan.Mapper.Exceptions;

namespace Anshan.Mapper
{
    public class ConditionalMapper : IConditionalMapper
    {
        private readonly IServiceProvider _serviceProvider;

        public ConditionalMapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ConditionalMapper<TInput, TOutput> For<TInput, TOutput>()
        {
            return new(_serviceProvider);
        }
    }

    public class ConditionalMapper<TInput, TOutput>
    {
        private readonly List<MapperDescriptor<TInput, TOutput>> _mapperDescriptors = new();
        private readonly IServiceProvider _serviceProvider;

        public ConditionalMapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ConditionalMapper<TInput, TOutput> MapWith<TMapper>(Func<bool> canMapEvaluator)
            where TMapper : IMapper<TInput, TOutput>
        {
            _mapperDescriptors.Add(new MapperDescriptor<TInput, TOutput>
            {
                Mapper = typeof(TMapper),
                CanMap = canMapEvaluator
            });

            return this;
        }

        public TOutput Execute(TInput input)
        {
            var result = _mapperDescriptors
                .FirstOrDefault(d => d.CanMap());

            if (result is null) throw new NoMapperCanMapException(typeof(TInput));

            var mapperInstance = _serviceProvider.GetService(result.Mapper) as IMapper<TInput, TOutput>;

            return mapperInstance.Map(input);
        }
    }
}