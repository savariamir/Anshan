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
    }
}