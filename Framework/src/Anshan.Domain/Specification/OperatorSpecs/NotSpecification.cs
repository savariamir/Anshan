namespace Anshan.Domain.Specification.OperatorSpecs
{
    public class NotSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _firstSpec;

        public NotSpecification(ISpecification<T> firstSpec)
        {
            _firstSpec = firstSpec;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return !_firstSpec.IsSatisfiedBy(candidate);
        }
    }
}