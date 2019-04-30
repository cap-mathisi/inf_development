using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Helpers.ReleaseState
{
    public class DraftReleaseStateChecker : IReleaseStateChecker
    {
        public bool PermissionIsAllowed(List<RoleTypes> roles, ProtocolPermissionTypes permission)
        {
            bool roleIsNotAdmin = (roles.Contains(RoleTypes.Admin) == false);
            bool roleIsNotAuthor = (roles.Contains(RoleTypes.Author) == false);

            if(roleIsNotAdmin && roleIsNotAuthor)
            {
                return false;
            }

            return true;
        }
    }
}
