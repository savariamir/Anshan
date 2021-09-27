using System.Threading.Tasks;
using Catalog.Query.ReadModel.Products;
using EasyPipe;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPipeline<string, ProductReadModel> _pipeline;

        public ProductController(IPipeline<string, ProductReadModel> pipeline)
        {
            _pipeline = pipeline;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(string id)
        {
            var product = await _pipeline.RunAsync(id);
            return Ok(product);
        }
    }
}