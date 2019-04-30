using System.Collections.Generic;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;

namespace sspx.web.Services
{
    public class RoleRepository : IRoleRepository
    {
        private ISSPxConfig _config;

        public RoleRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public List<Role> ListForUserProtocol(int userKey, int protocolKey)
        {
            var roles = SSPxDBHelper.GetUserRolesForProtocol(_config.SSPxConnectionString, userKey, protocolKey);
            roles = roles ?? new List<Role>();

            return roles;
        }

        public string Update(UserRole protocolVersionUserRole)
        {
            return SSPxDBHelper.SaveProtocalVersionUserRole(_config.SSPxConnectionString, protocolVersionUserRole);
        }
    }
}
