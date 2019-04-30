using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    public class InMemoryPagePermissions : IPagePermissions
    {
        private IConfiguration _temporaryConfig;
        private List<PagePermissionsLookup> _lookup;

        private class PagePermissionsLookup
        {
            public int UserKey;
            public string Url;
            public bool HasPermission;
        }

        public InMemoryPagePermissions(IConfiguration temporaryConfig)
        {
            _temporaryConfig = temporaryConfig;

            // this.Request.Path gives a string like "/Admin/ProtocolGroup"
            _lookup = new List<PagePermissionsLookup>
            {
                new PagePermissionsLookup
                {
                    UserKey = 194,
                    Url = "/protocols/permissionsdemo",
                    HasPermission = true
                },
                new PagePermissionsLookup
                {
                    UserKey = 116,
                    Url = "/protocols/permissionsdemo",
                    HasPermission = false
                },
                new PagePermissionsLookup
                {
                    UserKey = 203,
                    Url = "/protocols/permissionsdemo",
                    HasPermission = true
                }
            };
        }

        public bool HasPermission(int userKey, string path)
        {
            // TODO CS2:
            var permissionsBypass = string.Equals(_temporaryConfig["SSPX_PERMISSIONS_BYPASS_ALL"], "true", StringComparison.OrdinalIgnoreCase);
            if (permissionsBypass)
            {
                return true;
            }

            var query = _lookup.Where(p => p.UserKey == userKey && p.Url == path.ToLower());
            if (query.Any())
            {
                return query.First().HasPermission;
            }
            return false;
        }
    }
}
