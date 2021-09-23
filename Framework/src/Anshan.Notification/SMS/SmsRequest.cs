namespace Anshan.Notification.SMS
{
    public class SmsRequest
    {
        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public string Template { get; set; }
    }
}