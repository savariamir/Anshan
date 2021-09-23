using System.Collections.Generic;
using System.Linq;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Builders;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.Commands
{
    public class CommandCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            foreach (var useCase in context.DomainType.GetUseCases("Command"))
            foreach (var outputFile in Build(context, $"{useCase.Title}Command", useCase.Arguments, true))
                yield return outputFile;

            foreach (var outputFile in Build(context,
                                             $"Create{context.DomainName}Command",
                                             context.DomainType.GetData("Command"),
                                             withReturnType: true))
                yield return outputFile;

            foreach (var outputFile in Build(context,
                                             $"Delete{context.DomainName}Command",
                                             Enumerable.Empty<Property>(),
                                             true))
                yield return outputFile;
        }

        private IEnumerable<OutputFile> Build(DomainContext context,
                                              string commandName,
                                              IEnumerable<Property> properties,
                                              bool includeId = false,
                                              bool withReturnType = false)
        {
            var result = new CodeBuilder(context)
                         .WithTemplate("Command.liquid")
                         .WithFileName(commandName)
                         .WithPath(path =>
                         {
                             path.AtApplicationContracts()
                                 .Then(context.DomainName.InPlural);
                         })
                         .WithSharedPath(path =>
                         {
                             path.AtApplicationContracts()
                                 .Then("Shared");
                         })
                         .WithData("CommandName", commandName)
                         .WithData("Properties", properties)
                         .WithData("IncludeId", includeId)
                         .WithData("WithReturnType", withReturnType)
                         .Build();

            foreach (var property in properties.Where(p => p.IsUserDefinedType))
            {
                foreach (var outputFile in Build(context,
                                                 $"{property.InnerType.Name}Command",
                                                 property.InnerType.GetData("Command"))) yield return outputFile;

                foreach (var derivedType in property.InnerType.FindAllDerivedTypes())
                foreach (var outputFile in Build(context, $"{derivedType.Name}Command", derivedType.GetData("Command")))
                    yield return outputFile;
            }

            yield return result;
        }
    }
}