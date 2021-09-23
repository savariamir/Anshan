using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Controllers
{
    public class ControllerCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("Controller.liquid")
                         .WithFileName(context.DomainName + "Controller")
                         .WithPath(path =>
                         {
                             path.AtApi()
                                 .Then("Controllers");
                         })
                         .WithData("UseCases", context.DomainType.GetUseCases())
                         .Build();
        }
    }
}