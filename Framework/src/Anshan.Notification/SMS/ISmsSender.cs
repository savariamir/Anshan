using System.Threading.Tasks;

namespace Anshan.Notification.SMS
{
    public interface ISmsSender
    {
        Task SendSms(SmsRequest request);
    }
}