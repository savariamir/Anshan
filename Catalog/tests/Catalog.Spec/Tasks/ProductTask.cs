using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Catalog.Application.Contracts;

namespace Catalog.Spec.Tasks
{
    public class ProductTask
    {
        private readonly HttpClient _httpClient;

        public ProductTask(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        internal async Task<HttpResponseMessage> CreateProductAsync(CreateProductCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", command);
            return response;
        }
        
        internal async Task<HttpResponseMessage> UpdateProduct(UpdateProductCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync("api/products", command);
            return response;
        }

        public async Task<HttpResponseMessage> GetProductAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/products/{id}");
            return response;
        }
    }
}