#nullable enable

namespace Anshan.Domain
{
    /// <summary>
    ///     Represents an immutable string that cannot be null, empty or consisting of empty spaces
    /// </summary>
    public class Text : ValueObject
    {
        private readonly string _value;

        /// <summary>
        ///     Only for EF migrations
        /// </summary>
        private Text()
        {
        }

        private Text(string value)
        {
            _value = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public static implicit operator string(Text text)
        {
            return text._value;
        }

        public static implicit operator Text(string stringValue)
        {
            return new(stringValue);
        }

        public override bool Equals(object obj)
        {
            return Equals(_value, obj);
        }

        public override string ToString()
        {
            return _value;
        }
    }
}