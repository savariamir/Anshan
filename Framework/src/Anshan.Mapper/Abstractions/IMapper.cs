namespace Anshan.Mapper.Abstractions
{
    public interface IMapper
    {
        object Map(object source);
    }

    public interface IMapper<in TInput, out TOutput> : IMapper
    {
        object IMapper.Map(object source)
        {
            return Map((TInput) source);
        }

        TOutput Map(TInput command);
    }
}