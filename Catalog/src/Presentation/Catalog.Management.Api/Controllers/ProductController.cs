using System.Threading.Tasks;
using Catalog.Application.Contracts;
using Catalog.Query.Products;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Management.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICommandMediator _commandMediator;
        private readonly IQueryMediator _queryMediator;

        public ProductController(ICommandMediator commandMediator, IQueryMediator queryMediator)
        {
            _commandMediator = commandMediator;
            _queryMediator = queryMediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var results = await _queryMediator.QueryAsync(new GetProducts());
            return Ok(results);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var results = await _queryMediator.QueryAsync(new GetProductByIdQuery(){Id=id});
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var id = await _commandMediator.SendAsync(command);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand command)
        {
             await _commandMediator.SendAsync(command);
             return Ok();
        }
    }
}