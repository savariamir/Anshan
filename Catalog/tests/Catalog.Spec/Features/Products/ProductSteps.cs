using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Anshan.Extensions;
using Anshan.Test.Faker;
using Catalog.Application.Contracts;
using Catalog.Query.Model.Products;
using Catalog.Spec.Tasks;
using FluentAssertions;

namespace Catalog.Spec.Features.Products
{
    public class ProductSteps
    {
        private readonly ProductTask _task;
        
        private readonly ContextData _contextData = new();

        public ProductSteps(HttpClient httpClient)
        {
            _task = new ProductTask(httpClient);
        }

        public void AdminWantsToCreateAProduct()
        {
            var command = TestFaker.Build<CreateProductCommand>();
            _contextData.Set(command);
        }

        public async Task AdminSubmitsTheProduct()
        {
            var command = _contextData.Get<CreateProductCommand>();
            var response = await _task.CreateProductAsync(command);
            
            var id = await response.Content.ReadFromJsonAsync<string>();
            _contextData.SetStringId(id);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        public async Task ProductShouldBeCreatedCorrectly()
        {
            var command = _contextData.Get<CreateProductCommand>();
            var id =   _contextData.GetStringId();

            var response = await _task.GetProductAsync(id);
            
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var queryModel = response.ReadResponse<ProductQueryModel>();

            queryModel.Name.Should().Be(command.Name);
            queryModel.Description.Should().Be(command.Description);
            queryModel.Price.Should().Be(command.Price);
            queryModel.Quantity.Should().Be(command.Quantity);
            queryModel.Vendor.Should().Be(command.Vendor);
            queryModel.ProductAttributes.Should().NotBeEmpty().And.HaveCount(command.ProductAttributes.Count);
            queryModel.ProductAttributes.Should().Equal(command.ProductAttributes, (
                    actual, expected) => actual.Key == expected.Key
                                         && actual.Value == expected.Value);
        }
    }
    
}