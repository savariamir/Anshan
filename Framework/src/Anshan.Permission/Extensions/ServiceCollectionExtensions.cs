using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Permission.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFakeCurrentUser(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUser, FakeCurrentUser>();
            return services;
        }
    }
}