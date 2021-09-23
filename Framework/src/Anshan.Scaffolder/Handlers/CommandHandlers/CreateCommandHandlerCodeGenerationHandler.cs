using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.CommandHandlers
{
    public class CreateCommandHandlerCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("CreateCommandHandler.liquid")
                         .WithFileName($"Create{context.DomainName}CommandHandler")
                         .WithPath(path =>
                         {
                             path.AtApplication()
                                 .Then(context.DomainName.InPlural)
                                 .Then("Create");
                         })
                         .WithData("Properties", context.DomainType.GetData("Options"))
                         .Build();
        }
    }
}