using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Spec
{
    public class SpecTestCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("SpecTests.liquid")
                         .WithFileName($"{context.DomainName}Tests")
                         .WithPath(path =>
                         {
                             path.AtSpecs()
                                 .Then(context.DomainName.InPlural);
                         })
                         .WithData("UseCases", context.DomainType.GetUseCases())
                         .Build();
        }
    }
}