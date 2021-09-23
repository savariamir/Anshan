using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.CommandHandlers
{
    public class DeleteCommandHandlerCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("DeleteCommandHandler.liquid")
                         .WithFileName($"Delete{context.DomainName}CommandHandler")
                         .WithData("IncludeId", true)
                         .WithPath(path =>
                         {
                             path.AtApplication()
                                 .Then(context.DomainName.InPlural)
                                 .Then("Delete");
                         })
                         .Build();
        }
    }
}