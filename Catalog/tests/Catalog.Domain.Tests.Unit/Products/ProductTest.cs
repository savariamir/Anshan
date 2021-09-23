using System.Collections.Generic;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Builders;
using Catalog.Domain.Products.Options;
using FluentAssertions;
using Xunit;

namespace Catalog.Domain.Tests.Unit.Products
{
    public class ProductTest
    {
        [Fact]
        public void Should_create_a_product_properly()
        {
            var attributeOptions = new List<ProductAttributeOptions>
            {
                new()
                {
                    Key = "key1",
                    Value = "value1"
                },
                new()
                {
                    Key = "key2",
                    Value = "value2"
                }
            };

            var options = new ProductOptionsBuilder()
                .SetName("iPhone13")
                .SetDescription("This is a cell phone")
                .SetPrice(1000000M)
                .SetQuantity(2)
                .SetVendor("anshan company")
                .SetAttributes(attributeOptions)
                .Build();

            var product = Product.Create(options);

            product.Name.Should().Be("iPhone13");
            product.Description.Should().Be("This is a cell phone");
            product.Price.Value.Should().Be(1000000M);
            product.Quantity.Should().Be(2);
            product.Vendor.Should().Be("anshan company");
            product.ProductAttributes.Should().NotBeEmpty().And.HaveCount(2);
            product.ProductAttributes.Should().Equal(attributeOptions, (
                    actual, expected) => actual.Key == expected.Key
                                         && actual.Value == expected.Value);
        }
        
        [Fact]
        public void Should_update_a_product_properly()
        {
            var attributeOptions = new List<ProductAttributeOptions>
            {
                new()
                {
                    Key = "key1",
                    Value = "value1"
                },
                new()
                {
                    Key = "key2",
                    Value = "value2"
                }
            };

            var options = new ProductOptionsBuilder()
                .SetName("iPhone13")
                .SetDescription("This is a cell phone")
                .SetPrice(1000000M)
                .SetQuantity(2)
                .SetVendor("anshan company")
                .SetAttributes(attributeOptions)
                .Build();

            var product = Product.Create(options);
            var newAttributeOptions = new List<ProductAttributeOptions>
            {
                new()
                {
                    Key = "key1",
                    Value = "value1"
                },
                new()
                {
                    Key = "key2",
                    Value = "value2"
                },
                new()
                {
                    Key = "key3",
                    Value = "value3"
                }
            };
            var newOptions = new ProductOptionsBuilder()
                .SetName("iPhone12")
                .SetDescription("This is an old cell phone")
                .SetPrice(1200000M)
                .SetQuantity(3)
                .SetVendor("company")
                .SetAttributes(newAttributeOptions)
                .Build();
            
            product.Update(newOptions);

            product.Name.Should().Be("iPhone12");
            product.Description.Should().Be("This is an old cell phone");
            product.Price.Value.Should().Be(1200000M);
            product.Quantity.Should().Be(3);
            product.Vendor.Should().Be("company");
            product.ProductAttributes.Should().NotBeEmpty().And.HaveCount(3);
            product.ProductAttributes.Should().Equal(newAttributeOptions, (
                    actual, expected) => actual.Key == expected.Key
                                         && actual.Value == expected.Value);
        }
    }
}