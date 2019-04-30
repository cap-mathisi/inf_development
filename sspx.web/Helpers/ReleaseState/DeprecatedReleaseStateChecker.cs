using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Helpers.ReleaseState
{
    public class DeprecatedReleaseStateChecker : IReleaseStateChecker
    {
        public bool PermissionIsAllowed(List<RoleTypes> roles, ProtocolPermissionTypes permission)
        {
            bool roleIsNotAdmin = (roles.Contains(RoleTypes.Admin) == false);
            bool permissionIsEditOrComment = (
                (permission == ProtocolPermissionTypes.EditTemplate) ||
                (permission == ProtocolPermissionTypes.EditAll) ||
                (permission == ProtocolPermissionTypes.EditProtocol) ||
                (permission == ProtocolPermissionTypes.EditMetadata) || 
                (permission == ProtocolPermissionTypes.CreateEditViewComments)
            );

            if (roleIsNotAdmin && permissionIsEditOrComment)
            {
                return false;
            }

            return true;
        }
    }
}
