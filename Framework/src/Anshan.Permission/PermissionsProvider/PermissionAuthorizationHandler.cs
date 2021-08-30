using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace Anshan.Permission.PermissionsProvider
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IHostEnvironment _environment;
        private readonly IPermissionManager _permissionManager;

        public PermissionAuthorizationHandler(IPermissionManager permissionManager, IHostEnvironment environment)
        {
            _permissionManager = permissionManager;
            _environment = environment;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                             PermissionRequirement requirement)
        {
            if (_environment.IsDevelopment() || _environment.EnvironmentName == "Testing")
            {
                context.Succeed(requirement);
                return;
            }

            var user = context.User;
            if (!user.Identity.IsAuthenticated) context.Fail();

            // var userId = _permissionUserManager.GetUserId(_httpContextAccessor.HttpContext.User);

            //if (string.IsNullOrEmpty(userId))
            //{
            //    context.Fail();
            //    return Task.CompletedTask;
            //}

            var hasPermission = await _permissionManager.HasPermission("test", requirement.Permission);

            if (hasPermission)
                context.Succeed(requirement);
            else
                context.Fail();
        }
    }
}