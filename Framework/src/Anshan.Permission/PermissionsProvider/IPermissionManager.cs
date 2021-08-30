using System.Threading.Tasks;

namespace Anshan.Permission.PermissionsProvider
{
    public interface IPermissionManager
    {
        Task<bool> HasPermission(string userId, string permission);
    }
}