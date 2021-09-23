using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Anshan.Notification.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public Task SendEmailAsync(MailRequest mailRequest)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            mailMessage.To.Add(new MailboxAddress(mailRequest.ToEmail, mailRequest.ToEmail));
            mailMessage.Subject = mailRequest.Subject;
            mailMessage.Body = new TextPart("plain")
            {
                Text = mailRequest.Body
            };

            using var smtpClient = new SmtpClient();
            smtpClient.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.SslOnConnect);
            smtpClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);

            return Task.CompletedTask;
        }
    }
}