using System.Collections.Generic;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;

namespace sspx.web.Services
{
    public class ProtocolPermissionRepository : IProtocolPermissionRepository
    {
        private ISSPxConfig _config;

        public ProtocolPermissionRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public List<ProtocolPermission> ListForUserProtocol(int userKey, int protocolKey)
        {
            var permissions = SSPxDBHelper.GetUserPermissionsForProtocol(_config.SSPxConnectionString, userKey, protocolKey);
            return permissions ?? new List<ProtocolPermission>();
        }
    }
}
