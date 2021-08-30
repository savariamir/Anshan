using System.Collections.Generic;

namespace Catalog.Domain.Products.Options
{
    public class ProductOptions
    {
        public string Name { get;  set; }
        public string Description { get; set; }
        public string Vendor { get;  set; }
        public decimal Price { get;  set; }
        public int Quantity { get;  set; }
        public IList<ProductAttributeOptions> Attributes { get; set; }
    }
}