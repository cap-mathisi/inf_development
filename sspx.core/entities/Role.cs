using System;

namespace sspx.core.entities
{
    public class Role
    {
        public int RoleKey = DefaultValue.Key;
        public string RoleName = String.Empty;

        public static RoleTypes ToRoleTypes(Role role)
        {
            return (RoleTypes)role.RoleKey;
        }
    }
}
