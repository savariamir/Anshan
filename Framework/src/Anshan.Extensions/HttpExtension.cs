using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Anshan.Extensions
{
    public static class HttpExtension
    {
        public static T ReadResponse<T>(this HttpResponseMessage response)
        {
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

        public static async Task<HttpResponseMessage> PostAsync<TRequest>(this HttpClient client, string url,
                                                                          TRequest body)
        {
            var myContent = JsonConvert.SerializeObject(body);
            var response = await client.PostAsync(url, new StringContent(myContent, Encoding.UTF8, "application/json"));
            return response;
        }
    }
}