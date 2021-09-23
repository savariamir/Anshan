using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Spec
{
    public class SpecUseCaseStepsCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            foreach (var useCase in context.DomainType.GetUseCases())
                yield return new CodeBuilder(context)
                             .WithTemplate("SpecSteps.UseCase.liquid")
                             .WithFileName($"{context.DomainName}Tests.{useCase.Title.Value}")
                             .WithPath(path =>
                             {
                                 path.AtSpecs()
                                     .Then(context.DomainName.InPlural);
                             })
                             .PreventSharing()
                             .WithData("UseCase", useCase)
                             .Build();
        }
    }
}