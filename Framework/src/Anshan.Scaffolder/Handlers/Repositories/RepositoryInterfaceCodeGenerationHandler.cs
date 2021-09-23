using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Repositories
{
    public class RepositoryInterfaceCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("RepositoryInterface.liquid")
                         .WithFileName("I" + context.DomainName + "Repository")
                         .WithPath(path =>
                         {
                             path.AtDomain()
                                 .Then(context.DomainName.InPlural);
                         })
                         .Build();
        }
    }
}