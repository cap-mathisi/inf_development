using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Helpers.ReleaseState
{
    public class ModelingReleaseStateChecker : IReleaseStateChecker
    {
        public bool PermissionIsAllowed(List<RoleTypes> roles, ProtocolPermissionTypes permission)
        {
            bool roleIsNotAdmin = (roles.Contains(RoleTypes.Admin) == false);
            bool roleIsNotModeler = (roles.Contains(RoleTypes.Modeler) == false);
            bool permissionIsEdit = (
                (permission == ProtocolPermissionTypes.EditTemplate) ||
                (permission == ProtocolPermissionTypes.EditAll) ||
                (permission == ProtocolPermissionTypes.EditProtocol) ||
                (permission == ProtocolPermissionTypes.EditMetadata)
            );

            if (roleIsNotAdmin && roleIsNotModeler && permissionIsEdit)
            {
                return false;
            }

            return true;
        }
    }
}
