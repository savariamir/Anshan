using System;

namespace Anshan.Mapper
{
    internal class MapperDescriptor<TInput, TOutput>
    {
        public Type Mapper { get; set; }

        public Func<bool> CanMap { get; set; }
    }
}