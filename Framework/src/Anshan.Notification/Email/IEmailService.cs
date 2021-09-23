using System.Threading.Tasks;

namespace Anshan.Notification.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}