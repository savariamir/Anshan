using PhoneNumbers;

namespace Anshan.PhoneNumber
{
    public class PhoneNumberSpecification
    {
        public bool IsSatisfiedBy(string candidate)
        {
            if (string.IsNullOrEmpty(candidate))
                return false;

            var phoneUtil = PhoneNumberUtil.GetInstance();
            var iranNumberProto = phoneUtil.Parse(candidate, "IR");
            var isValid = phoneUtil.IsValidNumber(iranNumberProto);

            return isValid;
        }
    }
}