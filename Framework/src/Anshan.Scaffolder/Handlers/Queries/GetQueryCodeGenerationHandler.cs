using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Queries
{
    public class GetQueryCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("GetQuery.liquid")
                         .WithFileName("Get" + context.DomainName + "Query")
                         .WithPath(path =>
                         {
                             path.AtQuery()
                                 .Then(context.DomainName.InPlural);
                         })
                         .Build();
        }
    }
}