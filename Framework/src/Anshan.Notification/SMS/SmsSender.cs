using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Anshan.Notification.SMS
{
    public class SmsSender : ISmsSender
    {
        private readonly HttpClient _httpClient;
        private readonly SmsSettings _mailSettings;

        public SmsSender(IOptions<SmsSettings> mailSettings, IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient();
            _mailSettings = mailSettings.Value;
        }

        public async Task SendSms(SmsRequest request)
        {
            var url = $"https://api.kavenegar.com/v1/{_mailSettings.ApiKey}/verify/lookup.json";
            var param = $"?receptor={request.PhoneNumber}&token={request.Message}&template={request.Template}";
            await _httpClient.PatchAsync(url + param, new StringContent("", Encoding.UTF8, "application/json"));
        }
    }
}