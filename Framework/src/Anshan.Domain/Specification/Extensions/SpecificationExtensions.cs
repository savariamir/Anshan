using System.Linq;

namespace Anshan.Domain.Specification.Extensions
{
    public static class SpecificationExtensions
    {
        public static bool IsAllSatisfiedBy<TPropertyType>(this ISpecification<TPropertyType> specification,
                                                           params TPropertyType[] values)
        {
            return values.All(specification.IsSatisfiedBy);
        }
    }
}