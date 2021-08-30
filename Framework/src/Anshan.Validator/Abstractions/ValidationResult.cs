using System;
using System.ComponentModel.DataAnnotations;

namespace Anshan.Validator
{
    /// <summary>
    ///     Container class for the results of a validation request.
    /// </summary>
    /// <seealso cref="ValidationAttribute.GetValidationResult" />
    public class ValidationResult
    {
        /// <summary>
        ///     Constructor that accepts an error message.
        /// </summary>
        /// <param name="message">The user-visible error message.</param>
        public ValidationResult(string message, string key)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            Message = message;
            Key = key;
        }

        /// <summary>
        ///     Gets the error message for this result.
        /// </summary>
        public string Message { get; }

        public string Key { get; }


        /// <summary>
        ///     Override the string representation of this instance, returning the <see cref="Message" />
        /// </summary>
        /// <returns>The <see cref="Message" /> property value</returns>
        public override string ToString()
        {
            return Message;
        }
    }
}