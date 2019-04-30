namespace sspx.core.entities
{
    // <Aggregate>User/UserRoles
    public class UserRole
    {
        public int UserKey = DefaultValue.Key;
        public string FirstName = string.Empty;
        public string LastName = string.Empty;
        public RoleTypes Role;
        public int ProtocolVersionUserRoleKey = DefaultValue.Key;
        public bool? Assignreviewerflag;
    }
}

