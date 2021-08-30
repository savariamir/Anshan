namespace Anshan.Domain.Specification.Common
{
    public class NumberRangeSpecification : CompositeSpecification<int>
    {
        public NumberRangeSpecification(int min,
                                        int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }

        public int Max { get; }

        public override bool IsSatisfiedBy(int candidate)
        {
            return candidate >= Min && candidate <= Max;
        }
    }
}