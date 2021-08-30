using System;
using System.IO;
using System.Reflection;
using AutoFixture.Kernel;

namespace Anshan.Test.Faker
{
    internal class StreamGenerator : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            if (typeof(Stream).GetTypeInfo().IsAssignableFrom(request as Type))
                return null;
            return new NoSpecimen();
        }
    }
}