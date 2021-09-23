using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Anshan.Scaffolder.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsList(this Type type)
        {
            return type.GetInterface(nameof(IEnumerable)) != null && type != typeof(string) && !type.IsArray;
        }

        public static bool IsUserDefinedType(this Type type)
        {
            var actualType = type;

            if (type.IsList()) actualType = type.GetGenericArguments()[0];

            return actualType.IsClass && !actualType.FullName.StartsWith("System.");
        }

        public static IEnumerable<Type> FindAllDerivedTypes(this Type type)
        {
            return type.Assembly
                       .GetTypes()
                       .Where(t => t.IsSubclassOf(type));
        }
    }
}