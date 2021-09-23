using System.Collections.Generic;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Extensions
{
    public static class PropertyExtensions
    {
        public static void AddPostfix(this IEnumerable<Property> properties, string postfix)
        {
            foreach (var property in properties) property.AddPostfix(postfix);
        }
    }
}