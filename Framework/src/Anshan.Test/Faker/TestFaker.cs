using System.Runtime.Serialization;
using AutoFixture;

namespace Anshan.Test.Faker
{
    public static class TestFaker
    {
        private static readonly Fixture Fixture;

        static TestFaker()
        {
            Fixture = new Fixture();
            Fixture.Customizations.Add(new UtcGenerator());
            Fixture.Customizations.Add(new StreamGenerator());
        }

        public static T Build<T>()
        {
            return Fixture.Create<T>();
        }

        public static T ForceBuild<T>()
        {
            return (T) FormatterServices.GetUninitializedObject(typeof(T));
        }
    }
}