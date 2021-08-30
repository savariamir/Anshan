using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Anshan.Test
{
    public static class HttpExtension
    {
        public static T ReadResponse<T>(this HttpResponseMessage response)
        {
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        public static async Task<HttpResponseMessage> PostAsync<TRequest>(
            this HttpClient client, string url, TRequest body)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var myContent = JsonConvert.SerializeObject(body,
                                                        new JsonSerializerSettings
                                                        {
                                                            ContractResolver = contractResolver,
                                                            Formatting = Formatting.Indented
                                                        });

            var response = await client.PostAsync(url, new StringContent(myContent, Encoding.UTF8, "application/json"));
            return response;
        }

        public static async Task<HttpResponseMessage> PutAsync<TRequest>(
            this HttpClient client, string url, TRequest body)
        {
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            var myContent = JsonConvert.SerializeObject(body,
                                                        new JsonSerializerSettings
                                                        {
                                                            ContractResolver = contractResolver,
                                                            Formatting = Formatting.Indented
                                                        });

            var response = await client.PutAsync(url, new StringContent(myContent, Encoding.UTF8, "application/json"));
            return response;
        }
    }
}