using sspx.core.entities;

namespace sspx.web.Services
{
    public interface IProtocolPermissions
    {
        bool HasPermission(int userKey, int protocolKey, ProtocolPermissionTypes permission);
    }
}
