using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Validator.FluentValidation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //TODO: the following method may violates the single responsibility principle

        /// <summary>
        ///     Register FluentValidation validators from the specified assembly and
        ///     Registers the fluent validation as the default provider of <see cref="Anshan.Validator.IValidator" />
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection" /></param>
        /// <param name="assemblyContainingValidators">The assembly to look for <see cref="AbstractValidator{T}" /> implementations</param>
        /// <returns></returns>
        public static IServiceCollection AddFluentValidation(this IServiceCollection services,
                                                             Assembly assemblyContainingValidators)
        {
            services.AddValidatorsFromAssembly(assemblyContainingValidators);
            services.AddTransient<IValidator, FluentValidator>();

            return services;
        }
    }
}