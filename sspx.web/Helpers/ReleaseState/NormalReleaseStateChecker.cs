using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Helpers.ReleaseState
{
    public class NormalReleaseStateChecker : IReleaseStateChecker
    {
        public bool PermissionIsAllowed(List<RoleTypes> roles, ProtocolPermissionTypes permission)
        {
            bool roleIsNotAdmin = (roles.Contains(RoleTypes.Admin) == false);
            bool permissionIsNotViewOrComment =
                (permission != ProtocolPermissionTypes.View) &&
                (permission != ProtocolPermissionTypes.CreateEditViewComments);

            if (roleIsNotAdmin && permissionIsNotViewOrComment)
            {
                return false;
            }

            return true;
        }
    }
}
