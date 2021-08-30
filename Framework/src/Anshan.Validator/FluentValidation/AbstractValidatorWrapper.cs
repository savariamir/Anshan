using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anshan.Validator.FluentValidation
{
    internal abstract class AbstractValidatorWrapper
    {
        public abstract Task<IEnumerable<ValidationResult>> ValidateAsync(object validatorObj, object valueObj);
    }

    internal class AbstractValidatorWrapper<T> : AbstractValidatorWrapper where T : class
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(object validatorObj, object valueObj)
        {
            var validator = validatorObj as global::FluentValidation.IValidator<T> ??
                            throw new ArgumentException(nameof(validatorObj));
            var value = valueObj as T ?? throw new ArgumentException(nameof(valueObj));

            var result = await validator.ValidateAsync(value);

            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, e.PropertyName));
        }
    }
}