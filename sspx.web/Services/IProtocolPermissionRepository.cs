using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public interface IProtocolPermissionRepository
    {
        List<ProtocolPermission> ListForUserProtocol(int userKey, int protocolKey);
    }
}
