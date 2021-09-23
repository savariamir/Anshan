using System;
using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Handlers.CommandHandlerTests
{
    public class CreateCommandHandlerTestsCodeGenerationHandler : ICodeGenerationHandler
    {
        public IEnumerable<OutputFile> Handle(DomainContext context)
        {
            throw new NotImplementedException();

            // context.CreateUseCaseSignature("command");
            // context.AddPostfixToProperties("Options");
            //
            // var model = new
            // {
            //     context.SolutionName,
            //     context.DomainName,
            //     context.Properties
            // };
            //
            //
            // yield return new OutputFile
            // {
            //     TemplateName = "CreateCommandHandlerTests.liquid",
            //     FileName = "Create" + context.DomainName + "CommandHandlerTests",
            //     Model = model,
            //     FilePath = new PathBuilder(context.RootPath)
            //                .AtApplicationTests(context.SolutionName)
            //                .Then(context.DomainName.InPlural)
            //                .Build()
            // };
        }
    }
}