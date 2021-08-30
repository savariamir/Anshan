using System.Text.RegularExpressions;

namespace Anshan.Domain.Specification.Common
{
    public class LetterOnlySpecification : CompositeSpecification<string>
    {
        public override bool IsSatisfiedBy(string candidate)
        {
            var regex = new Regex(@"^[a-zA-Z]+$");
            return regex.IsMatch(candidate);
        }
    }
}