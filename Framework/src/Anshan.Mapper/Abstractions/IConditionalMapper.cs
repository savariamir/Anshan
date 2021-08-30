namespace Anshan.Mapper.Abstractions
{
    public interface IConditionalMapper
    {
        public ConditionalMapper<TInput, TOutput> For<TInput, TOutput>();
    }
}