using System.Linq;
using Catalog.Application.Contracts;
using Catalog.Domain.Products.Builders;
using Catalog.Domain.Products.Options;

namespace Catalog.Application.Products
{
    public static class ProductOptionsFactory
    {
        public static ProductOptions CreateFrom(CreateProductCommand command)
        {
            var attributeOptions = command.ProductAttributes
                .Select(p => new ProductAttributeOptions {Key = p.Key, Value = p.Value}).ToList();

            var options = new ProductOptionsBuilder()
                .SetName(command.Name)
                .SetDescription(command.Description)
                .SetPrice(command.Price)
                .SetQuantity(command.Quantity)
                .SetVendor(command.Vendor)
                .SetAttributes(attributeOptions)
                .Build();

            return options;
        }

        public static ProductOptions CreateFrom(UpdateProductCommand command)
        {
            var attributeOptions = command.ProductAttributes
                .Select(p => new ProductAttributeOptions {Key = p.Key, Value = p.Value}).ToList();

            var options = new ProductOptionsBuilder()
                .SetName(command.Name)
                .SetDescription(command.Description)
                .SetPrice(command.Price)
                .SetQuantity(command.Quantity)
                .SetVendor(command.Vendor)
                .SetAttributes(attributeOptions)
                .Build();

            return options;
        }
    }
}