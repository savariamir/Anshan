using System;
using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.CommandHandlerTests
{
    public class DeleteCommandHandlerTestsCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            throw new NotImplementedException();

            // var model = new
            // {
            //     context.SolutionName,
            //     context.DomainName,
            // };
            //
            // yield return new OutputFile
            // {
            //     TemplateName = "DeleteCommandHandlerTests.liquid",
            //     FileName = "Delete" + context.DomainName + "CommandHandlerTests",
            //     Model = model,
            //     FilePath = new PathBuilder(context.RootPath)
            //                .AtApplicationTests(context.SolutionName)
            //                .Then(context.DomainName.InPlural)
            //                .Build()
            // };
        }
    }
}