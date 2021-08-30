using System.Collections.Generic;
using Anshan.Core;
using Catalog.Domain.Products.Options;

namespace Catalog.Domain.Products.Builders
{
    public class ProductOptionsBuilder : Builder<ProductOptions>
    {
        public ProductOptionsBuilder SetName(string name)
        {
            SetValue(p=>p.Name, name);
            return this;
        }
        
        public ProductOptionsBuilder SetDescription(string description)
        {
            SetValue(p=>p.Description, description);
            return this;
        }
        
        public ProductOptionsBuilder SetVendor(string vendor)
        {
            SetValue(p=>p.Vendor, vendor);
            return this;
        }
        
        public ProductOptionsBuilder SetPrice(decimal price)
        {
            SetValue(p=>p.Price, price);
            return this;
        }
        
        public ProductOptionsBuilder SetAttributes(List<ProductAttributeOptions> options)
        {
            SetValue(p=>p.Attributes, options);
            return this;
        }

        public ProductOptionsBuilder SetQuantity(int quantity)
        {
            SetValue(p => p.Quantity, quantity);
            return this;
        }
    }
}