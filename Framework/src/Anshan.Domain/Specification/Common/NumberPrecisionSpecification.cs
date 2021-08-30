using System;
using System.Globalization;

namespace Anshan.Domain.Specification.Common
{
    public class NumberPrecisionSpecification : CompositeSpecification<decimal>
    {
        public NumberPrecisionSpecification(int maxPrecision)
        {
            MaxPrecision = maxPrecision;
            MaxScale = MaxPrecision - 1;
        }

        public NumberPrecisionSpecification(int maxPrecision,
                                            int maxScale)
        {
            MaxPrecision = maxPrecision;
            MaxScale = maxScale;
        }

        public int MaxPrecision { get; }

        public int MaxScale { get; }

        public override bool IsSatisfiedBy(decimal candidate)
        {
            var number = candidate.ToString(CultureInfo.InvariantCulture);

            int totalDigits;
            int numberOfDecimals;

            if (number.Contains("."))
            {
                totalDigits = number.Length - 1;
                numberOfDecimals = number.Length - number.IndexOf(".", StringComparison.Ordinal) - 1;
            }
            else
            {
                totalDigits = number.Length;
                numberOfDecimals = 0;
            }

            return totalDigits <= MaxPrecision && numberOfDecimals <= MaxScale;
        }
    }
}