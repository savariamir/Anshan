using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.CommandHandlers
{
    public class CommandHandlerCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            foreach (var useCase in context.DomainType.GetUseCases(useCasePrefixSignature: "command"))
                yield return new CodeBuilder(context)
                             .WithTemplate("CommandHandler.liquid")
                             .WithFileName(useCase.Title + "CommandHandler")
                             .WithPath(path =>
                             {
                                 path.AtApplication()
                                     .Then(context.DomainName.InPlural)
                                     .Then(useCase.Title);
                             })
                             .PreventSharing()
                             .WithData("UseCase", useCase)
                             .Build();
        }
    }
}