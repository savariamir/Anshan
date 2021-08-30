namespace Anshan.Domain.Specification
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}