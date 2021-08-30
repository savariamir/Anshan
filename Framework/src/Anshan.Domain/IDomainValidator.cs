namespace Anshan.Domain
{
    public interface IDomainValidator
    {
        bool IsValid { get; }
    }
}