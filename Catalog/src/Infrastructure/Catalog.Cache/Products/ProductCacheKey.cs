namespace Catalog.Cache.Products
{
    public static class ProductCacheKey
    {
        private const string Suffix = "catalog:pr";

        public static string GetProduct(string id) => $"{Suffix}:{id}";
    }
}