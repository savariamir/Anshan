using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.CommandHandlerTests
{
    public class CommandHandlerTestCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            foreach (var useCase in context.DomainType.GetUseCases(useCasePrefixSignature: "command"))
                yield return new CodeBuilder(context)
                             .WithTemplate("CommandHandlerTest.liquid")
                             .WithFileName(useCase.Title + "CommandHandlerTests")
                             .WithPath(path =>
                             {
                                 path.AtApplicationTests()
                                     .Then(context.DomainName.InPlural);
                             })
                             .PreventSharing()
                             .WithData("UseCase", useCase)
                             .Build();
        }
    }
}