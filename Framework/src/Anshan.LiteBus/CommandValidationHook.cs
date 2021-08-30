using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Validator;
using LiteBus.Commands.Abstractions;
using LiteBus.Messaging.Abstractions;

namespace Anshan.LiteBus
{
    public class CommandValidationHook : ICommandPreHandleHook
    {
        private readonly IValidator _validator;

        public CommandValidationHook(IValidator validator)
        {
            _validator = validator ?? throw new NoValidatorSpecifiedException();
        }

        public async Task ExecuteAsync(IMessage message, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _validator.ValidateAsync(message);

            if (result.Any())
                // TODO: return a feasible error
                throw new Exception();
        }
    }
}