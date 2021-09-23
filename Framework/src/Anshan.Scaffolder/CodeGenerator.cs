using System;
using System.Collections.Generic;
using System.Linq;
using Anshan.Scaffolder.Configuration;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder
{
    public static class CodeGenerator
    {
        public static void Generate(Action<GenerationConfiguration> configBuilder)
        {
            var generationBuilder = new GenerationConfiguration();
            configBuilder(generationBuilder);

            var outputFiles = new List<OutputFile>();

            foreach (var configItem in generationBuilder.Configurations)
            {
                var generationContext = new DomainContext(configItem.Key);

                foreach (var codeBuilder in configItem.Value.CodeBuilders)
                {
                    var generatedOutputFiles = codeBuilder.Handle(generationContext).ToList();

                    outputFiles.AddRange(generatedOutputFiles);
                }
            }

            var result = outputFiles.GroupBy(o => o.FileName)
                                    .Where(g => g.Count() > 1)
                                    .ToList();

            if (result.Any())
                outputFiles.ForEach(f => f.HasShared = true);

            foreach (var grouping in result)
            foreach (var outputFile in grouping)
                outputFile.IsShared = true;

            outputFiles.ForEach(o => o.Create());
        }
    }
}