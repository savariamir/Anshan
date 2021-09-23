using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Spec
{
    public class SpecCreateStepsCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("SpecSteps.Create.liquid")
                         .WithFileName($"{context.DomainName}Tests.Create")
                         .WithPath(path =>
                         {
                             path.AtSpecs()
                                 .Then(context.DomainName.InPlural);
                         })
                         .PreventSharing()
                         .Build();
        }
    }
}