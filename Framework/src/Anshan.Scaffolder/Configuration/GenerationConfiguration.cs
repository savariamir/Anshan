using System;
using System.Collections.Generic;

namespace Anshan.Scaffolder.Configuration
{
    public class GenerationConfiguration
    {
        internal Dictionary<Type, DomainGenerationConfiguration> Configurations { get; } = new();

        public GenerationConfiguration For<TDomain>()
        {
            var builder = new DomainGenerationConfiguration()
                          .WithOptions()
                          //.WithConstructors()
                          .WithQueryModels()
                          .WithQueryHandlers()
                          .WithCommands()
                          .WithCommandHandlers()
                          .WithControllers()
                          .WithRepositoryImplementations()
                          .WithCommandHandlerTests()
                          .WithRepositoryInterfaces()
                          .WithSpecs();

            Configurations[typeof(TDomain)] = builder;
            return this;
        }

        public GenerationConfiguration For<TDomain>(Action<DomainGenerationConfiguration> configBuilder)
        {
            var configuration = new DomainGenerationConfiguration();
            configBuilder(configuration);

            Configurations[typeof(TDomain)] = configuration;
            return this;
        }
    }
}