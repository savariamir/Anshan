using Anshan.Domain.Exceptions;

namespace Anshan.Domain
{
    public static class DomainValidatorExtensions
    {
        public static void Validate(this IDomainValidator validator)
        {
            if (!validator.IsValid)
                throw new ValidationFailedException();
        }
    }
}