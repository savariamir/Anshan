using System;
using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Queries
{
    public class QueryModelCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            return Build(context, context.DomainType);
        }

        public IEnumerable<OutputFile> Build(DomainContext context, Type typeUnderWork)
        {
            var result = new CodeBuilder(context)
                         .WithTemplate("QueryModel.liquid")
                         .WithFileName(typeUnderWork.Name + "QueryModel")
                         .WithPath(path =>
                         {
                             path.AtQueryModels()
                                 .Then(context.DomainName.InPlural);
                         })
                         .WithSharedPath(path =>
                         {
                             path.AtQueryModels()
                                 .Then("Shared");
                         })
                         .WithData("QueryModelName", $"{typeUnderWork.Name}QueryModel")
                         .WithData("Properties", typeUnderWork.GetData("QueryModel"))
                         .Build();

            foreach (var userDefinedType in typeUnderWork.GetUserDefinedTypes())
            {
                foreach (var outputFile in Build(context, userDefinedType)) yield return outputFile;

                foreach (var derivedType in userDefinedType.FindAllDerivedTypes())
                foreach (var outputFile in Build(context, derivedType))
                    yield return outputFile;
            }

            yield return result;
        }
    }
}