using Microsoft.Extensions.Configuration;
using sspx.core.entities;
using System;

namespace sspx.web.Services
{
    public class AdminPermissions : IAdminPermissions
    {
        private IConfiguration _temporaryConfig;

        public AdminPermissions(IConfiguration temporaryConfig)
        {
            _temporaryConfig = temporaryConfig;

        }

        public AdminMenuPermissionTypes GetForUser(int userKey)
        {
            // TODO CS2:
            var permissionsBypass = string.Equals(_temporaryConfig["SSPX_PERMISSIONS_BYPASS_ALL"], "true", StringComparison.OrdinalIgnoreCase);
            if (permissionsBypass)
            {
                return AdminMenuPermissionTypes.StaffAdmin;
            }

            // TODO CS2:
            return AdminMenuPermissionTypes.StaffAdmin;
        }
    }
}
