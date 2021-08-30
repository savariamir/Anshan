using Anshan.Domain.Specification.OperatorSpecs;

namespace Anshan.Domain.Specification
{
    public abstract class CompositeSpecification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T candidate);

        public CompositeSpecification<T> And(ISpecification<T> secondSpec)
        {
            return new AndSpecification<T>(this, secondSpec);
        }

        public CompositeSpecification<T> Or(ISpecification<T> secondSpec)
        {
            return new OrSpecification<T>(this, secondSpec);
        }

        public CompositeSpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
}