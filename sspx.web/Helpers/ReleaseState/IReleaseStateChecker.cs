using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Helpers.ReleaseState
{
    public interface IReleaseStateChecker
    {
        bool PermissionIsAllowed(List<RoleTypes> roles, ProtocolPermissionTypes permission);
    }
}
