namespace Catalog.Cache
{
    public class CacheResponse<T>
    {
        public CacheResponse(T data)
        {
            Data = data;
        }

        public T Data { private set; get; }

        public bool Hint => Data is not null;
    }
}