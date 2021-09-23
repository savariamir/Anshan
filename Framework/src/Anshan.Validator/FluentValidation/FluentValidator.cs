using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anshan.Validator.Abstractions;
using Anshan.Validator.FluentValidation.Exceptions;
using FluentValidation;
using IValidator = Anshan.Validator.Abstractions.IValidator;

namespace Anshan.Validator.FluentValidation
{
    public class FluentValidator : IValidator
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task<IEnumerable<ValidationResult>> ValidateAsync<T>(T value)
        {
            if (!(_serviceProvider.GetService(typeof(IValidator<T>)) is IValidator<T> validator))
                throw new ValidatorNotFoundException(typeof(T));

            var result = await validator.ValidateAsync(value);

            return result.Errors.Select(e => new ValidationResult(e.ErrorMessage, e.PropertyName));
        }

        public async Task<IEnumerable<ValidationResult>> ValidateAsync(object value)
        {
            var wrapperType = typeof(AbstractValidatorWrapper<>)
                .MakeGenericType(value.GetType());

            var validatorType = typeof(IValidator<>)
                .MakeGenericType(value.GetType());

            var validator = _serviceProvider.GetService(validatorType);

            if (validator is null) throw new ValidatorNotFoundException(value.GetType());

            var wrapper = Activator.CreateInstance(wrapperType) as AbstractValidatorWrapper;

            return await wrapper!.ValidateAsync(validator, value);
        }
    }
}