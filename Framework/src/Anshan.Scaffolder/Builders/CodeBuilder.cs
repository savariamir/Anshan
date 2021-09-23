using System;
using Anshan.Scaffolder.Configuration;
using Anshan.Scaffolder.Extensions;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Builders
{
    public class CodeBuilder
    {
        private readonly OutputFile _outputFile = new();
        private readonly PathBuilder _pathBuilder = new();
        private readonly PathBuilder _sharedPathBuilder = new();

        public CodeBuilder(DomainContext domainContext)
        {
            _outputFile.Model.Add("DomainName", domainContext.DomainName);
            _outputFile.Model.Add("SolutionName", SolutionInfo.SolutionName);
        }

        public CodeBuilder WithTemplate(string templateName)
        {
            _outputFile.TemplateName = templateName;
            return this;
        }

        public CodeBuilder WithPath(Action<PathBuilder> pathBuilderConfiguration)
        {
            pathBuilderConfiguration(_pathBuilder);

            _outputFile.FilePath = _pathBuilder.Build();

            return this;
        }

        public CodeBuilder WithSharedPath(Action<PathBuilder> pathBuilderConfiguration)
        {
            pathBuilderConfiguration(_sharedPathBuilder);

            _outputFile.SharedFilePath = _sharedPathBuilder.Build();

            return this;
        }

        public CodeBuilder WithFileName(string fileName)
        {
            _outputFile.FileName = fileName;

            return this;
        }

        public CodeBuilder WithData(string name, object model)
        {
            _outputFile.Model.Add(name, model);

            return this;
        }

        public OutputFile Build()
        {
            return _outputFile;
        }

        public CodeBuilder PreventSharing()
        {
            _outputFile.PreventShared = true;

            return this;
        }
    }
}