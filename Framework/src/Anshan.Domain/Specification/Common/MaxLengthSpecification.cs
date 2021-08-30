namespace Anshan.Domain.Specification.Common
{
    public class MaxLengthSpecification : CompositeSpecification<string>
    {
        private readonly int _maxLength;

        public MaxLengthSpecification(int maxLength)
        {
            _maxLength = maxLength;
        }

        public override bool IsSatisfiedBy(string candidate)
        {
            return candidate.Length <= _maxLength;
        }
    }
}