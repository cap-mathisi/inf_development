using System.Collections.Generic;
using System.Linq;
using sspx.core.entities;

namespace sspx.web.Services
{
    public class InMemoryProtocolPermissionRepository : IProtocolPermissionRepository
    {
        private List<UserProtocolPermission> _lookup;

        private class UserProtocolPermission
        {
            public int UserKey;
            public int ProtocolKey;
            public ProtocolPermission Permission;
        }

        public InMemoryProtocolPermissionRepository()
        {
            _lookup = new List<UserProtocolPermission>
            {
                #region admin (simulated "author" and "reviewer" roles)
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 5, // breast
                    Permission = new ProtocolPermission {PermissionKey=7, PermissionText="EditProtocol"}
                },
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 5, // breast
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 7, // colon res
                    Permission = new ProtocolPermission {PermissionKey=7, PermissionText="EditProtocol"}
                },
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 7, // colon res
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                //new UserProtocolPermission
                //{
                //    UserKey = 194, // admin
                //    ProtocolKey = 52, // Urethra
                //    Permission = new ProtocolPermission {PermissionKey=7, PermissionText="EditProtocol"}
                //},
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 52, // Urethra
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                //new UserProtocolPermission
                //{
                //    UserKey = 194, // admin
                //    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                //    Permission = new ProtocolPermission {PermissionKey=7, PermissionText="EditProtocol"}
                //},
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 36, // Pharynx
                    Permission = new ProtocolPermission {PermissionKey=7, PermissionText="EditProtocol"}
                },
                new UserProtocolPermission
                {
                    UserKey = 194, // admin
                    ProtocolKey = 36, // Pharynx
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                #endregion admin

                #region test user (simulated "reviewer" role)
                new UserProtocolPermission
                {
                    UserKey = 116,
                    ProtocolKey = 5, // breast
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 116,
                    ProtocolKey = 7, // colon res
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 116,
                    ProtocolKey = 52, // Urethra
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 116,
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 116,
                    ProtocolKey = 36, // Pharynx
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                #endregion

                #region staff user  (simulated "reviewer" role)
                new UserProtocolPermission
                {
                    UserKey = 203,
                    ProtocolKey = 5, // breast
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 203,
                    ProtocolKey = 7, // colon res
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 203,
                    ProtocolKey = 52, // Urethra
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 203,
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                },
                new UserProtocolPermission
                {
                    UserKey = 203,
                    ProtocolKey = 36, // Pharynx
                    Permission = new ProtocolPermission {PermissionKey=8, PermissionText="View"}
                }

                #endregion
            };
        }

        public List<ProtocolPermission> ListForUserProtocol(int userKey, int protocolKey)
        {
            var permissions = (
                from item in _lookup
                where item.UserKey == userKey && item.ProtocolKey == protocolKey
                select item.Permission
            ).ToList();

            // allow View even if they have no roles
            if( permissions.Where(p => p.PermissionKey == 8).Count() == 0)
            {
                permissions.Add(new ProtocolPermission { PermissionKey = 8, PermissionText = "View" });
            }

            return permissions;
        }
    }
}
