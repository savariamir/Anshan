using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Spec
{
    public class SpecTaskCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("SpecTask.liquid")
                         .WithFileName($"{context.DomainName}Task")
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