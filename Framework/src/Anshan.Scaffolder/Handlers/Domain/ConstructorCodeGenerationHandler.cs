using System;
using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Domain
{
    public class ConstructorCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            return Build(context, context.DomainType);
        }

        public IEnumerable<OutputFile> Build(DomainContext context, Type typeUnderWork)
        {
            var result = new CodeBuilder(context)
                         .WithTemplate("Constructor.liquid")
                         .WithFileName($"{typeUnderWork.Name}.Constructor")
                         .WithPath(path =>
                         {
                             path.AtDomain()
                                 .Then(context.DomainName.InPlural);
                         })
                         .WithSharedPath(path =>
                         {
                             path.AtDomain()
                                 .Then("Shared");
                         })
                         .PreventSharing()
                         .WithData("ObjectName", typeUnderWork.Name)
                         .WithData("Properties", typeUnderWork.GetData())
                         .Build();

            foreach (var userDefinedType in typeUnderWork.GetUserDefinedTypes())
            foreach (var outputFile in Build(context, userDefinedType))
                yield return outputFile;

            yield return result;
        }
    }
}