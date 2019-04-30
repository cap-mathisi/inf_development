using sspx.core.entities;

namespace sspx.web.Services
{
    public interface IAdminPermissions
    {
        AdminMenuPermissionTypes GetForUser(int userKey);
    }
}
