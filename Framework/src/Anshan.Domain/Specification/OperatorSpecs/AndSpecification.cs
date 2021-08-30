namespace Anshan.Domain.Specification.OperatorSpecs
{
    public class AndSpecification<T> : CompositeSpecification<T>
    {
        private readonly ISpecification<T> _firstSpec;
        private readonly ISpecification<T> _secondSpec;

        public AndSpecification(ISpecification<T> firstSpec,
                                ISpecification<T> secondSpec)
        {
            _firstSpec = firstSpec;
            _secondSpec = secondSpec;
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return _firstSpec.IsSatisfiedBy(candidate) && _secondSpec.IsSatisfiedBy(candidate);
        }
    }
}