using System.Threading;
using System.Threading.Tasks;
using Catalog.Application.Contracts;
using Catalog.Domain.Products;
using LiteBus.Commands.Abstractions;

namespace Catalog.Application.Products
{
    public class UpdateProductHandler : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(UpdateProductCommand command, CancellationToken cancellationToken = new())
        {
            var product = await _repository.GetByIdAsync(command.Id);
  
            var options = ProductOptionsFactory.CreateFrom(command);
            product.Update(options);
            
            await _repository.UpdateAsync(product);
        }
    }
}