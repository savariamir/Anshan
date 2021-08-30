using System;
using System.Reflection;
using AutoFixture.Kernel;

namespace Anshan.Test.Faker
{
    internal class UtcGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (typeof(DateTime).GetTypeInfo().IsAssignableFrom(request as Type))
                return DateTime.UtcNow;
            return new NoSpecimen();
        }
    }
}