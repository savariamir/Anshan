using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Validator;
using Anshan.Validator.Abstractions;
using LiteBus.Commands.Abstractions;
using LiteBus.Messaging.Abstractions;

namespace Anshan.LiteBus
{
    public class CommandValidationHook : ICommandPreHandler
    {
        private readonly IValidator _validator;

        public CommandValidationHook(IValidator validator)
        {
            _validator = validator ?? throw new NoValidatorSpecifiedException();
        }

        public async Task PreHandleAsync(IHandleContext<ICommandBase> context)
        {
            var result = await _validator.ValidateAsync(context.Message);

            if (result.Any())
                // TODO: return a feasible error
                throw new Exception();
        }
    }
}