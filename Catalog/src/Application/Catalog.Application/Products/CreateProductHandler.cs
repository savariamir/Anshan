using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Contracts;
using Catalog.Domain.Products;
using LiteBus.Commands.Abstractions;

namespace Catalog.Application.Products
{
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken = new())
        {
            var options = ProductOptionsFactory.CreateFrom(command);
            var product = Product.Create(options);
            await _repository.AddAsync(product);
            
            return product.Id;
        }
    }
}