using Catalog.Management.Api;
using Microsoft.AspNetCore.Mvc.Testing;
using TestStack.BDDfy;
using Xunit;

namespace Catalog.Spec.Features.Products
{
    [Collection("SpecTest")]
    public class ProductTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ProductSteps _steps;

        public ProductTest(WebApplicationFactory<Startup> factory)
        {
            _steps = new ProductSteps(factory.CreateClient());
        }


        [Fact]
        public void Create_product()
        {
            this.Given(_ => _steps.AdminWantsToCreateAProduct())
                .When(_ => _steps.AdminSubmitsTheProduct())
                .Then(_ => _steps.ProductShouldBeCreatedCorrectly());
        }
    }
}