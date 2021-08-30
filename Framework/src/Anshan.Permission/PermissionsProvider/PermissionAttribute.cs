using Microsoft.AspNetCore.Authorization;

namespace Anshan.Permission.PermissionsProvider
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(object permissionType)
        {
            Policy = permissionType.ToString();
        }
    }
}