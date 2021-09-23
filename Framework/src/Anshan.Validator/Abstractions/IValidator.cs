using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anshan.Validator.Abstractions
{
    /// <summary>
    ///     Represents the validator interface
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        ///     Validates a given object
        /// </summary>
        /// <param name="value">The object to validate</param>
        /// <typeparam name="T">The type of object</typeparam>
        /// <returns>Validated errors</returns>
        Task<IEnumerable<ValidationResult>> ValidateAsync<T>(T value);

        Task<IEnumerable<ValidationResult>> ValidateAsync(object value);
    }
}