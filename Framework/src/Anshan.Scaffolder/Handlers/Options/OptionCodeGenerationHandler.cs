using System;
using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Options
{
    public class OptionCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            return Build(context, context.DomainType);
        }

        public IEnumerable<OutputFile> Build(DomainContext context, Type typeUnderWork)
        {
            var result = new CodeBuilder(context)
                         .WithTemplate("Options.liquid")
                         .WithFileName(typeUnderWork.Name + "Options")
                         .WithPath(path =>
                         {
                             path.AtDomain()
                                 .Then(context.DomainName.InPlural)
                                 .Then("Options");
                         })
                         .WithSharedPath(path =>
                         {
                             path.AtDomain()
                                 .Then("Shared")
                                 .Then("Options");
                         })
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