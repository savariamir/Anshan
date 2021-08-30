namespace Anshan.Query
{
    public static class QueryExistence
    {
        public static void EnsureIsNotNull<T>(this T model)
        {
            if (model is null)
                throw new NotFoundResource();
        }
    }
}