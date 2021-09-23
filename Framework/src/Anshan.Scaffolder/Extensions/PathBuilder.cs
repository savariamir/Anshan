using System.Collections.Generic;
using System.IO;
using Anshan.Scaffolder.Configuration;

namespace Anshan.Scaffolder.Extensions
{
    public class PathBuilder
    {
        private readonly List<string> _pathSections = new();

        public PathBuilder()
        {
            _pathSections.Add(SolutionInfo.RootPath);
        }

        public PathBuilder Then(string pathSection)
        {
            _pathSections.Add(pathSection);
            return this;
        }

        public PathBuilder Then(params string[] pathSections)
        {
            _pathSections.AddRange(pathSections);
            return this;
        }

        public PathBuilder AtQuery()
        {
            _pathSections.Add("src");
            _pathSections.Add("Infrastructure");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Query");

            return this;
        }

        public PathBuilder AtApplication()
        {
            _pathSections.Add("src");
            _pathSections.Add("Application");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Application");

            return this;
        }

        public PathBuilder AtApplicationTests()
        {
            _pathSections.Add("tests");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Application.Tests.Unit");

            return this;
        }

        public string Build()
        {
            return Path.Combine(_pathSections.ToArray());
        }

        public PathBuilder AtApplicationContracts()
        {
            _pathSections.Add("src");
            _pathSections.Add("Application");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Application.Contracts");

            return this;
        }

        public PathBuilder AtControllers()
        {
            _pathSections.Add("src");
            _pathSections.Add("Presentation");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Api");
            _pathSections.Add("Controllers");

            return this;
        }

        public PathBuilder AtApi()
        {
            _pathSections.Add("src");
            _pathSections.Add("Presentation");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Api");

            return this;
        }

        public PathBuilder AtDomain()
        {
            _pathSections.Add("src");
            _pathSections.Add("Domain");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Domain");

            return this;
        }

        public PathBuilder AtQueryModels()
        {
            _pathSections.Add("src");
            _pathSections.Add("Infrastructure");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Query.Models");

            return this;
        }

        public PathBuilder AtMongo()
        {
            _pathSections.Add("src");
            _pathSections.Add("Infrastructure");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Persistence.Mongo");

            return this;
        }

        public PathBuilder AtSpecs()
        {
            _pathSections.Add("tests");
            _pathSections.Add($"{SolutionInfo.SolutionName}.Specs");

            return this;
        }
    }
}