using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using sspx.core.entities;

namespace sspx.web.Services
{
    public class InMemoryAdminPermissions : IAdminPermissions
    {
        private IConfiguration _temporaryConfig;
        private Dictionary<int, AdminMenuPermissionTypes> _userAdminMenuPermissions;

        public InMemoryAdminPermissions(IConfiguration temporaryConfig)
        {
            _temporaryConfig = temporaryConfig;

            _userAdminMenuPermissions = new Dictionary<int, AdminMenuPermissionTypes>
            {
                // admin
                { 194, AdminMenuPermissionTypes.StaffAdmin },

                // test
                { 116, AdminMenuPermissionTypes.PrimaryAuthorAdmin },
                
                // staff
                { 203, AdminMenuPermissionTypes.None },
            };
        }

        public AdminMenuPermissionTypes GetForUser(int userKey)
        {
            // TODO CS2:
            var permissionsBypass = string.Equals(_temporaryConfig["SSPX_PERMISSIONS_BYPASS_ALL"], "true", StringComparison.OrdinalIgnoreCase);
            if (permissionsBypass)
            {
                return AdminMenuPermissionTypes.StaffAdmin;
            }

            if (_userAdminMenuPermissions.ContainsKey(userKey) == false)
            {
                return AdminMenuPermissionTypes.None;
            }

            return _userAdminMenuPermissions[userKey];
        }
    }
}
