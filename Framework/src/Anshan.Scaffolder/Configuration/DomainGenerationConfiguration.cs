using System.Collections.Generic;
using Anshan.Scaffolder.Abstractions;
using Anshan.Scaffolder.Handlers.CommandHandlers;
using Anshan.Scaffolder.Handlers.CommandHandlerTests;
using Anshan.Scaffolder.Handlers.Commands;
using Anshan.Scaffolder.Handlers.Controllers;
using Anshan.Scaffolder.Handlers.Domain;
using Anshan.Scaffolder.Handlers.Options;
using Anshan.Scaffolder.Handlers.Queries;
using Anshan.Scaffolder.Handlers.Repositories;
using Anshan.Scaffolder.Handlers.Spec;

namespace Anshan.Scaffolder.Configuration
{
    public class DomainGenerationConfiguration
    {
        internal List<ICodeGenerationHandler> CodeBuilders { get; } = new();

        public DomainGenerationConfiguration WithOptions()
        {
            CodeBuilders.Add(new BuilderCodeGenerationHandler());
            CodeBuilders.Add(new OptionCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithConstructors()
        {
            CodeBuilders.Add(new ConstructorCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithCommands()
        {
            CodeBuilders.Add(new CommandCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithCommandHandlers()
        {
            CodeBuilders.Add(new CommandHandlerCodeGenerationHandler());
            CodeBuilders.Add(new DeleteCommandHandlerCodeGenerationHandler());
            CodeBuilders.Add(new CreateCommandHandlerCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithCommandHandlerTests()
        {
            CodeBuilders.Add(new CommandHandlerTestCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithControllers()
        {
            CodeBuilders.Add(new ControllerCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithQueryModels()
        {
            CodeBuilders.Add(new QueryModelCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithQueryHandlers()
        {
            CodeBuilders.Add(new GetQueryCodeGenerationHandler());
            CodeBuilders.Add(new GetAllQueryCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithSpecs()
        {
            CodeBuilders.Add(new SpecTaskCodeGenerationHandler());
            CodeBuilders.Add(new SpecTestCodeGenerationHandler());
            CodeBuilders.Add(new SpecCreateStepsCodeGenerationHandler());
            CodeBuilders.Add(new SpecDeleteStepsCodeGenerationHandler());
            CodeBuilders.Add(new SpecUseCaseStepsCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithRepositoryInterfaces()
        {
            CodeBuilders.Add(new RepositoryInterfaceCodeGenerationHandler());

            return this;
        }

        public DomainGenerationConfiguration WithRepositoryImplementations()
        {
            CodeBuilders.Add(new RepositoryImplementationCodeGenerationHandler());

            return this;
        }
    }
}