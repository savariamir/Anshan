using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Anshan.Permission.PermissionsProvider
{
    public static class SecurityExtenstion
    {
        public static void AddPermissions(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        }
    }
}