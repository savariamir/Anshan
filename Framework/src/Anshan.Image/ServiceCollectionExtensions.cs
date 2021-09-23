using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Image
{
    public static class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddBlurHashGenerator(this IServiceCollection services)
        {
            services.AddTransient<IBlurHashGenerator, BlurHashGenerator>();

            return services;
        } 
    }
}