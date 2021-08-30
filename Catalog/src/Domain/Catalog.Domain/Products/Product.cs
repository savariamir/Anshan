using System.Collections.Generic;
using System.Linq;
using Anshan.Domain;
using Catalog.Domain.Products.Options;

namespace Catalog.Domain.Products
{
    public class Product : AggregateRoot<string>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Vendor { get; private set; }
        public Money Price { get; private set; }
        public int Quantity { get; private set; }

        public IReadOnlyCollection<ProductAttribute> ProductAttributes => _productAttributes;

        private List<ProductAttribute> _productAttributes = new();

        private Product(ProductOptions options)
        {
            Update(options);
        }
        
        public static Product Create(ProductOptions options) => new(options);

        public void Update(ProductOptions options)
        {
            Name = options.Name;
            Description = options.Description;
            Vendor = options.Vendor;
            Price = options.Price;
            Quantity = options.Quantity;
            _productAttributes.Update(options.Attributes.Select(p=>new ProductAttribute(p)).ToList());
        }
    }
}