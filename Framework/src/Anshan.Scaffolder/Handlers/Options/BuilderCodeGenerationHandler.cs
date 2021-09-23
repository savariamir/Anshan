using System;
using System.Collections.Generic;
using System.Linq;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Options
{
    public class BuilderCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            var result = Build(context, context.DomainType).ToList();

            return result;
        }

        public IEnumerable<OutputFile> Build(DomainContext context, Type typeUnderWork)
        {
            var result = new CodeBuilder(context)
                         .WithTemplate("Builder.liquid")
                         .WithFileName(typeUnderWork.Name + "Builder")
                         .WithPath(path =>
                         {
                             path.AtDomain()
                                 .Then(context.DomainName.InPlural)
                                 .Then("Builders");
                         })
                         .WithSharedPath(path =>
                         {
                             path.AtDomain()
                                 .Then("Shared")
                                 .Then("Builders");
                         })
                         .WithData("BuilderName", $"{typeUnderWork.Name}Builder")
                         .WithData("OptionsName", $"{typeUnderWork.Name}Options")
                         .WithData("Properties", typeUnderWork.GetData("Options"))
                         .Build();

            foreach (var userDefinedType in typeUnderWork.GetUserDefinedTypes())
            foreach (var outputFile in Build(context, userDefinedType))
                yield return outputFile;

            yield return result;
        }
    }
}