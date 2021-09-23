using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder.Extensions
{
    public static class DomainExtensions
    {
        private static readonly string[] ExcludedProperties =
        {
            "CreatedAt",
            "ModifiedAt",
            "IsDeleted",
            "DeletedAt",
            "GetHashCode",
            "Equals"
        };

        private static readonly string[] ExcludedMethods =
        {
            "Publish",
            "GetEvents",
            "Delete",
            "SetId",
            "GetHashCode",
            "Equals"
        };

        public static IEnumerable<Property> GetData(this Type type, string postfix = null)
        {
            var result = type
                         .GetProperties()
                         .Where(p => !ExcludedProperties.Contains(p.Name))
                         .Select(p => new Property(p))
                         .ToList();

            if (postfix is not null)
                foreach (var property in result)
                    property.AddPostfix(postfix);

            return result;
        }

        public static IEnumerable<UseCase> GetUseCases(this Type type, string argumentPostfix = null,
                                                       string useCasePrefixSignature = null)
        {
            var result = type
                         .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                         .Where(m => !m.IsSpecialName && m.DeclaringType != typeof(object))
                         .Where(m => !ExcludedMethods.Contains(m.Name))
                         .Select(m => new UseCase(m))
                         .ToList();

            if (argumentPostfix is not null)
                foreach (var useCase in result)
                foreach (var useCaseArgument in useCase.Arguments)
                    useCaseArgument.AddPostfix(argumentPostfix);

            if (useCasePrefixSignature is not null)
                foreach (var useCase in result)
                    useCase.SetSignature(useCasePrefixSignature);

            return result;
        }

        public static IEnumerable<Type> GetUserDefinedTypes(this Type type)
        {
            return type
                   .GetProperties()
                   .Where(p => !ExcludedProperties.Contains(p.Name))
                   .Select(p => new Property(p))
                   .Where(p => p.IsUserDefinedType)
                   .Select(p => p.InnerType)
                   .Distinct();
        }
    }
}