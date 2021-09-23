using Humanizer;

namespace Anshan.Scaffolder.Models
{
    public class MultiStyleText
    {
        public MultiStyleText(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public string InCamelCase => Value.Camelize();

        public string InKebabCase => Value.Kebaberize();

        public string InPascalCase => Value.Pascalize();

        public string InSnakeCase => Value.Underscore();

        public string InPlural => Value.Pluralize();

        public string InSingular => Value.Singularize();

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator MultiStyleText(string value)
        {
            return new(value);
        }

        public static implicit operator string(MultiStyleText value)
        {
            return value.Value;
        }
    }
}