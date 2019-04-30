using System.Collections.Generic;
using sspx.core.entities;

namespace sspx.web.Helpers.ReleaseState
{
    public class ReviewQA_ReleaseStateChecker : IReleaseStateChecker
    {
        public bool PermissionIsAllowed(List<RoleTypes> roles, ProtocolPermissionTypes permission)
        {
            bool roleIsNotAdmin = (roles.Contains(RoleTypes.Admin) == false);
            bool roleIsNotAuthor = (roles.Contains(RoleTypes.Author) == false);
            bool roleIsNotModeler = (roles.Contains(RoleTypes.Modeler) == false);
            bool roleIsNotReviewer = (roles.Contains(RoleTypes.Reviewer) == false);
            bool roleIsNotStaff = (roles.Contains(RoleTypes.CacPERTStaff) == false);
            bool permissionIsComment = (permission == ProtocolPermissionTypes.CreateEditViewComments);

            bool permissionIsEdit = (
                (permission == ProtocolPermissionTypes.EditTemplate) ||
                (permission == ProtocolPermissionTypes.EditAll) ||
                (permission == ProtocolPermissionTypes.EditProtocol) ||
                (permission == ProtocolPermissionTypes.EditMetadata)
            );

            if ( roleIsNotAdmin && roleIsNotAuthor && roleIsNotModeler && 
                 roleIsNotReviewer && roleIsNotStaff &&
                 permissionIsComment
            )
            {
                return false;
            }

            if (roleIsNotAdmin && roleIsNotAuthor && roleIsNotModeler && permissionIsEdit)
            {
                return false;
            }

            return true;
        }
    }
}
