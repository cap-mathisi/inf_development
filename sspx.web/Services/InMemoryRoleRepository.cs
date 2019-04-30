using System.Collections.Generic;
using System.Linq;
using sspx.core.entities;

namespace sspx.web.Services
{
    public class InMemoryRoleRepository : IRoleRepository
    {
        private List<UserProtocolRole> _lookup;

        private class UserProtocolRole
        {
            public int UserKey;
            public int ProtocolKey;
            public Role Role;
        }

        public InMemoryRoleRepository()
        {
            _lookup = new List<UserProtocolRole>
            {
                #region admin (simulated "author" and "admin" role)
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 5, // breast
                    Role = new Role { RoleKey = 1, RoleName = "Author"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 7, // colon res
                    Role = new Role { RoleKey = 1, RoleName = "Author"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 52, // Urethra
                    Role = new Role { RoleKey = 1, RoleName = "Author"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Role = new Role { RoleKey = 1, RoleName = "Author"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 36, // Pharynx
                    Role = new Role { RoleKey = 1, RoleName = "Author"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 5, // breast
                    Role = new Role { RoleKey = 5, RoleName = "Admin"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 7, // colon res
                    Role = new Role { RoleKey = 5, RoleName = "Admin"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 52, // Urethra
                    Role = new Role { RoleKey = 5, RoleName = "Admin"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Role = new Role { RoleKey = 5, RoleName = "Admin"}
                },
                new UserProtocolRole
                {
                    UserKey = 194, // admin
                    ProtocolKey = 36, // Pharynx
                    Role = new Role { RoleKey = 5, RoleName = "Admin"}
                },

                #endregion admin

                #region test user (simulated "reviewer" role)
                new UserProtocolRole
                {
                    UserKey = 116,
                    ProtocolKey = 5, // breast
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 116,
                    ProtocolKey = 7, // colon res
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 116,
                    ProtocolKey = 52, // Urethra
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 116,
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 116,
                    ProtocolKey = 36, // Pharynx
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                #endregion

                #region staff user  (simulated "reviewer" role)
                new UserProtocolRole
                {
                    UserKey = 203,
                    ProtocolKey = 5, // breast
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 203,
                    ProtocolKey = 7, // colon res
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 203,
                    ProtocolKey = 52, // Urethra
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 203,
                    ProtocolKey = 27, // Nasal Cavity and Paranasal Sinuses
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                },
                new UserProtocolRole
                {
                    UserKey = 203,
                    ProtocolKey = 36, // Pharynx
                    Role = new Role { RoleKey = 3, RoleName = "Reviewer"}
                }
                #endregion
            };
        }

        public List<Role> ListForUserProtocol(int userKey, int protocolKey)
        {
            var query = _lookup.Where(u =>
                u.UserKey == userKey &&
                u.ProtocolKey == protocolKey);

            if(query.Any() == false)
            {
                return new List<Role>();
            }

            return query.Select(u => u.Role).ToList();
        }

        public string Update(UserRole protocolVersionUserRole)
        {
            return string.Empty;
        }
    }
}
