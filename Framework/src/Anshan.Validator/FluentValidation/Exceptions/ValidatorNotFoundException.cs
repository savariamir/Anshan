using System;

namespace Anshan.Validator.FluentValidation.Exceptions
{
    [Serializable]
    public class ValidatorNotFoundException : Exception
    {
        public ValidatorNotFoundException(Type type) : base($"No validator found for '{type.Name}' type")
        {
        }
    }
}