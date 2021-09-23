using System.IO;
using System.Linq;

namespace Anshan.Scaffolder.Configuration
{
    public static class SolutionInfo
    {
        static SolutionInfo()
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (true)
            {
                var files = currentDirectory.GetFiles("*.sln", SearchOption.TopDirectoryOnly);

                if (files.Any())
                {
                    RootPath = currentDirectory.ToString();
                    SolutionName = Path.GetFileNameWithoutExtension(files.First().Name);

                    return;
                }

                currentDirectory = currentDirectory.Parent;
            }
        }

        public static string RootPath { get; }

        public static string SolutionName { get; }
    }
}