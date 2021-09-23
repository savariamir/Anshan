using System.Collections.Generic;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Extensions
{
    public static class UseCaseExtensions
    {
        public static void AddPostfix(this IEnumerable<UseCase> useCases, string postfix)
        {
            foreach (var useCase in useCases) useCase.AddPostfix(postfix);
        }
    }
}