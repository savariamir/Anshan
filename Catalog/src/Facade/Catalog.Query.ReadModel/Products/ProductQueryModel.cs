using System.Collections.Generic;

namespace Catalog.Query.ReadModel.Products
{
    public class ProductReadModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public IEnumerable<ProductAttributeReadModel> ProductAttributes { set; get; }
    }
}