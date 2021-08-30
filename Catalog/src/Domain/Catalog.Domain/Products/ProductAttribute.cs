using Anshan.Domain;
using Catalog.Domain.Products.Options;

namespace Catalog.Domain.Products
{
    public class ProductAttribute : ValueObject
    {
        public ProductAttribute(ProductAttributeOptions attribute)
        {
            Value = attribute.Value;
            Key = attribute.Key;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}