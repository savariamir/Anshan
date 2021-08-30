namespace Anshan.Extensions
{
    public static class ObjectExtension
    {
        public static bool IsNull<TSource>(this TSource source)
        {
            return source is null;
        }

        public static bool IsNotNull<TSource>(this TSource source)
        {
            return source is not null;
        }
    }
}