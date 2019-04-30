using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public interface IRoleRepository
    {
        List<Role> ListForUserProtocol(int userKey, int protocolKey);

        string Update(UserRole protocolVersionUserRole);
    }
}
