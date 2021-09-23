using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Repositories
{
    public class RepositoryImplementationCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("RepositoryImplementation.liquid")
                         .WithFileName(context.DomainName + "Repository")
                         .WithPath(path =>
                         {
                             path.AtMongo()
                                 .Then("Repositories");
                         })
                         .Build();
        }
    }
}