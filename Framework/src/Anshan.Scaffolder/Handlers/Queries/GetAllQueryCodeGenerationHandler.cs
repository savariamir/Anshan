using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Queries
{
    public class GetAllQueryCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            yield return new CodeBuilder(context)
                         .WithTemplate("GetAllQuery.liquid")
                         .WithFileName("GetAll" + context.DomainName + "Query")
                         .WithPath(path =>
                         {
                             path.AtQuery()
                                 .Then(context.DomainName.InPlural);
                         })
                         .Build();
        }
    }
}