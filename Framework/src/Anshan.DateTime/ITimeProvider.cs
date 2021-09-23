namespace Anshan.DateTime
{
    public interface IDateTimeProvider
    {
        System.DateTime Now { get; }

        string ConvertToLocalDateTime(System.DateTime utcDateTime);

        string ConvertToLocalDate(System.DateTime utcDateTime);

        string ConvertToLocalTime(System.DateTime utcDateTime);

        System.DateTime? ConvertToUtcDateTime(string persianDateTime);

        string ConvertToLocalDateTime(System.DateTime? utcDateTime);

        string ConvertToLocalDateWithFormat(System.DateTime utcDateTime);

        string ConvertToDateTimeFullString(System.DateTime utcDateTime);

        string ConvertToLocalDateTimeDifferenceNow(System.DateTime utcDateTime);
    }
}