using System.Collections.Generic;
using LiteBus.Commands.Abstractions;

namespace Catalog.Application.Contracts
{
    public class CreateProductCommand : ICommand<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<ProductAttributeCommand> ProductAttributes { set; get; }
    }
}