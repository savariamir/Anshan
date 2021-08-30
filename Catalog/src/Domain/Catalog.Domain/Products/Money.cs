using Anshan.Domain;

namespace Catalog.Domain.Products
{
    public class Money : ValueObject
    {
        public Money(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        public static implicit operator Money(decimal value) => new(value);
    }
}