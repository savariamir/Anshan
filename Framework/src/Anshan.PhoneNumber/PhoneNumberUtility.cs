using System.Net.Mail;
using System.Text.RegularExpressions;
using PhoneNumbers;

namespace Anshan.PhoneNumber
{
    public static class PhoneNumberUtility
    {
        public static string NormalizePhoneNumber(this string phoneNumber)
        {
            var phoneUtil = PhoneNumberUtil.GetInstance();
            var iranNumberProto = phoneUtil.Parse(phoneNumber, "IR");

            var normalizePhoneNumber = phoneUtil.Format(iranNumberProto, PhoneNumberFormat.E164);

            return normalizePhoneNumber;
        }

        public static bool IsValidPhoneNumber(this string number)
        {
            return Regex.Match(number, "^(\\+98|0)?9\\d{9}$").Success;
        }
    }

    public static class EmailExtension
    {
        public static bool IsValidEmail(this string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}