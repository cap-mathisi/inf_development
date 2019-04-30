using Microsoft.Extensions.Configuration;
using System;

namespace sspx.web.Services
{
    public class PagePermissions : IPagePermissions
    {
        private IConfiguration _temporaryConfig;

        public PagePermissions(IConfiguration temporaryConfig)
        {
            _temporaryConfig = temporaryConfig;
        }

        public bool HasPermission(int userKey, string path)
        {
            // TODO CS2:
            var permissionsBypass = string.Equals(_temporaryConfig["SSPX_PERMISSIONS_BYPASS_ALL"], "true", StringComparison.OrdinalIgnoreCase);
            if (permissionsBypass)
            {
                return true;
            }

            // TODO CS2:

            // TODO CS2:
            throw new NotImplementedException("This method to be implemented if we decide to store URL access permissions for a given user in the database.");
        }
    }
}
