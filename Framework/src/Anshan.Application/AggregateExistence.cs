using System.Collections.Generic;
using Anshan.Domain;

namespace Anshan.Application
{
    public static class AggregateExistence
    {
        public static void EnsureIsNotNull<T>(this T model) where T : IAggregate
        {
            if (model is null)
                throw new AggregateNotFound();
        }
        
        public static void EnsureIsNotNull<T>(this List<T> models) where T : IAggregate
        {
            if (models is null)
                throw new AggregateNotFound();
        }
    }
}