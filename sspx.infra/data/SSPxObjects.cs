using System;
using System.Data;
using sspx.core.entities;

namespace sspx.infra.data
{
    public static class ObjectHelper
    {
        #region - User
        public static User UserMapper(IDataRecord row)
        {
            var user = new User()
            {
                UserKey = row.GetInt32(0),
                UserID = row.GetString(1),
                FirstName = row.GetString(2),
                MiddleName = row.GetString(3),
                LastName = row.GetString(4),
                Email = row.GetString(5),
                WorkPhone = row.IsDBNull(6) ? string.Empty : row.GetString(6),
                HomePhone = row.IsDBNull(7) ? string.Empty : row.GetString(7),
                CellPhone = row.IsDBNull(8) ? string.Empty : row.GetString(8),
                UserTypeKey = row.IsDBNull(9) ? DefaultValue.Key : row.GetInt32(9),
                Qualifications = row.IsDBNull(10) ? string.Empty : row.GetString(10),
                VendorKey = row.IsDBNull(11) ? DefaultValue.Key : row.GetInt32(11),
                Specialties = row.IsDBNull(12) ? string.Empty : row.GetString(12),
                Active = row.GetBoolean(13)
            };

            return user;
        }

        public static UserType UserTypeMapper(IDataRecord row)
        {
            var userTypes = new UserType()
            {
                UserTypeKey = row.GetInt32(0),
                NamespaceKey = row.GetInt32(1),
                Type = row.GetString(2),
                Description = row.GetString(3),
                SortOrder = row.IsDBNull(4) ? DefaultValue.SortOrder : row.GetInt32(4),
                Active = row.GetBoolean(5)
            };

            return userTypes;
        }

        public static ProtocolPermission PermissionMapper(IDataRecord row)
        {
            var permission = new ProtocolPermission
            {
                PermissionKey = row.GetInt32(0),
                PermissionText = row.IsDBNull(1) ? string.Empty : row.GetString(1)
            };

            return permission;
        }

        public static ProtocolRoleData ProtocolRoleDataMapper(IDataRecord row)
        {
            var protocolRole = new ProtocolRoleData
            {
                ProtocolKey = row.GetInt32(0),
                RoleKey = row.GetInt32(1)
            };

            return protocolRole;
        }

        public static Role RoleMapper(IDataRecord row)
        {
            var role = new Role
            {
                RoleKey = row.GetInt32(0),
                RoleName = row.GetString(1)
            };

            return role;
        }

        #endregion

        #region - Protocols
        public static Protocol ProtocolMapper(IDataRecord row)
        {
            var protocol = new Protocol
            {
                ProtocolKey = row.GetInt32(0),
                NamespaceKey = row.IsDBNull(1) ? DefaultValue.Namespace : row.GetInt32(1),
                ProtocolName = row.GetString(2),
                ProtocolShortName = row.GetString(3),
                ProtocolSortName = row.GetString(4),
                TestProtocol = row.GetBoolean(5),
                CreatedBy = row.GetDecimal(6),
                // LastUpdated = row.GetDecimal(7), // TODO CS2: current data model is mistakenly a DATETIME. skipping for now (2018-11-30)
                Active = row.GetBoolean(8)
            };

            return protocol;
        }

        public static ProtocolWithGroup ProtocolWithGroupMapper(IDataRecord row)
        {
            var protocol = new ProtocolWithGroup
            {
                ProtocolKey = row.GetInt32(0),
                ProtocolName = row.GetString(1),
                ProtocolShortName = row.GetString(2),
                ProtocolSortName = row.IsDBNull(3) ? string.Empty : row.GetString(3),
                CreatedBy = row.GetDecimal(4),
                // LastUpdated = row.GetDecimal(5), // TODO CS2: current data model is mistakenly a DATETIME. skipping for now (2018-11-30)
                ProtocolActive = row.GetBoolean(6),
                ProtocolGroupKey = row.GetInt32(7),
                ProtocolGroupName = row.GetString(8),
                ProtocolGroupSortName = row.IsDBNull(9) ? string.Empty : row.GetString(9)
            };

            return protocol;
        }

        public static ProtocolGroup ProtocolGroupMapper(IDataRecord row)
        {
            var protocolGroup = new ProtocolGroup
            {
                ProtocolGroupKey = row.GetInt32(0),
                NamespaceKey = row.IsDBNull(1) ? DefaultValue.Namespace : row.GetInt32(1),
                ProtocolGroupName = row.GetString(2),
                ProtocolGroupSortName = row.IsDBNull(3) ? string.Empty : row.GetString(3),
                Active = row.GetBoolean(4)
            };

            return protocolGroup;
        }

        public static ProtocolIndexDataRow ProtocolIndexMapper(IDataRecord row)
        {
            var protocolIndexData = new ProtocolIndexDataRow
            {
                ProtocolKey = row.GetInt32(0),
                ProtocolName = row.GetString(1),
                ProtocolGroupName = row.GetString(2),
                ProtocolVersionKey = row.GetInt32(3),
                ProtocolVersionLastUpdatedDt = row.GetDateTime(4),
                RoleKey = row.GetInt32(5),
                ProtocolVersion = row.GetString(6),
                CommentsCount = row.GetInt32(7)
            };

            return protocolIndexData;
        }

        public static ProtocolVersion ProtocolVersionMapper(IDataRecord row)
        {
            var protocolVersion = new ProtocolVersion
            {
                ProtocolVersionKey = row.GetInt32(0),
                NamespaceKey = row.IsDBNull(1) ? DefaultValue.Namespace : row.GetInt32(1),
                ProtocolKey = row.GetInt32(2),
                ProtocolVersionText = row.GetString(3),
                TestVersion = row.GetBoolean(4),
                Title = row.GetString(5),
                SubTitle = row.GetString(6),
                ReleaseDate = null,
                WebPostingDate = null,
                ReleaseStatesKey = row.IsDBNull(9) ? ReleaseStateTypes.NULL : (ReleaseStateTypes)row.GetInt32(9),
                UserKey = row.IsDBNull(10) ? DefaultValue.Key : row.GetInt32(10),
                LastUpdated = row.IsDBNull(11) ? DefaultValue.Ckey : row.GetDecimal(11),
                LastUpdatedDt = row.GetDateTime(12),
                Active = row.GetBoolean(13)
            };

            if (row.IsDBNull(7) == false)
            {
                protocolVersion.ReleaseDate = row.GetDateTime(7);
            }
            if (row.IsDBNull(8) == false)
            {
                protocolVersion.WebPostingDate = row.GetDateTime(8);
            }

            return protocolVersion;
        }

        public static ProtocolVersionUserData ProtocolVersionUserMapper(IDataRecord row)
        {
            var protocolVersionUser = new ProtocolVersionUserData
            {
                ProtocolVersionKey = row.GetInt32(0),
                UserKey = row.GetInt32(1),
                FirstName = row.GetString(2),
                LastName = row.GetString(3),
                RoleKey = row.GetInt32(4)
            };
            return protocolVersionUser;
        }

        public static ProtocolVersionsStates ProtocolVersionsStatesMapper(IDataRecord row)
        {
            var protocolVersionsStates = new ProtocolVersionsStates
            {
                ProtocolversionStatusKey = row.IsDBNull(0) ? DefaultValue.Key : row.GetInt32(0),
                ProtocolversionKey = row.IsDBNull(1) ? DefaultValue.Key : row.GetInt32(1),
                ReleaseStatesKey = row.IsDBNull(2) ? DefaultValue.Key : row.GetInt32(2),
                CreatedDate = row.IsDBNull(3) ? (DateTime?)null : row.GetDateTime(3),
                CratedBy = row.IsDBNull(4) ? string.Empty : row.GetString(4),
                ModifiedDate = row.IsDBNull(5) ? (DateTime?)null : row.GetDateTime(5),
                ModifiedBy = row.IsDBNull(6) ? string.Empty : row.GetString(6),
                ProtocolVersion = row.IsDBNull(7) ? string.Empty : row.GetString(7),
                ReleaseStatus = row.IsDBNull(8) ? string.Empty : row.GetString(8)
            };
            return protocolVersionsStates;
        }

        public static ChecklistReviewers ChecklistReviewersMapper(IDataRecord row)
        {
            var checklistReviewers = new ChecklistReviewers
            {
                ReviewerId = row.GetInt32(0),
                ReviewerName = row.IsDBNull(1) ? string.Empty : row.GetString(1),
                ProtocolVersionUserRoleKey = row.GetInt32(2),
                Selected = row.IsDBNull(3) ? false : row.GetBoolean(3),
                FirstName = row.GetString(4),
                Email = row.GetString(5),
                ProtocolName = row.GetString(6)
            };
            return checklistReviewers;
        }

        public static LablelistAuthors LablelistAuthorsMapper(IDataRecord row)
        {
            var lablelistAuthors = new LablelistAuthors
            {
                AuthorId = row.GetInt32(0),
                AuthorName = row.IsDBNull(1) ? string.Empty : row.GetString(1),
                ProtocolVersionUserRoleKey = row.GetInt32(2),
                FirstName = row.GetString(3),
                Email = row.GetString(4),
                ProtocolName = row.GetString(5)
            };
            return lablelistAuthors;
        }

        public static ProtocolVersion AssignReviewerMapper(IDataRecord row)
        {
            var assignReviewer = new ProtocolVersion
            {
                ReviewStartDate = row.IsDBNull(0) ? (DateTime?)null : row.GetDateTime(0),// row.GetDateTime(0),

                ReviewEndDate = row.IsDBNull(1) ? (DateTime?)null : row.GetDateTime(1),
                CustomMessage = row.IsDBNull(2) ? string.Empty : row.GetString(2)
            };

            return assignReviewer;
        }
        #endregion

        #region - Notes
        public static Note NoteMapper(IDataRecord row)
        {
            var note = new Note
            {
                NoteCkey = row.GetDecimal(0),
                NoteKey = row.GetInt32(1),
                Namespace = row.GetInt32(2),
                DraftVersion = row.GetDecimal(3),
                //BaseVersion = row.GetDecimal(4), // NAA. Resolve nulls later.
                NoteNumber = row.GetString(4),
                NoteTitle = row.GetString(5),
                //NoteDetails = row.GetString(7), // NAA.  This is varbinary???
                ProtocolVersionCkey = row.GetDecimal(6),
                Active = row.GetBoolean(7)
            };

            return note;
        }

        #endregion

        #region Admin

        public static AdminCaseSummary AdminCaseSummary(IDataRecord row)
        {
            var protocolVersion = new AdminCaseSummary
            {
                TemplateVersionKey = row.GetInt32(0)
            };
            return protocolVersion;
        }
        public static Qualification QualificationMapper(IDataRecord row)
        {
            var qualification = new Qualification
            {
                QualificationKey = row.GetInt32(0),
                QualificationTxt = row.GetString(1),
                Description = row.IsDBNull(2) ? string.Empty : row.GetString(2),
                CreatedBy = row.IsDBNull(3) ? DefaultValue.Ckey : row.GetDecimal(3),
                LastUpdated = row.GetDecimal(4),
                Active = row.GetBoolean(5)
            };

            return qualification;
        }

        public static Specialty SpecialtyMapper(IDataRecord row)
        {
            var specialty = new Specialty
            {
                SpecialtyKey = row.GetInt32(0),
                SpecialtyTxt = row.GetString(1).Trim(),
                Description = row.IsDBNull(2) ? string.Empty : row.GetString(2).Trim()
            };

            return specialty;
        }

        public static Standard StandardMapper(IDataRecord row)
        {
            var standard = new Standard
            {
                BasedOnCkey = row.GetDecimal(0),
                BasedOnKey = row.GetInt32(1),
                Namespace = row.GetInt32(2),
                BasedOn = row.GetString(3),
                Description = row.IsDBNull(4) ? string.Empty : row.GetString(4),
                SortOrder = row.GetInt32(5),
                Active = row.GetBoolean(6)
            };

            return standard;
        }

        public static Vendor VendorMapper(IDataRecord row)
        {
            var vendor = new Vendor
            {
                VendorKey = row.GetInt32(0),
                VendorTxt = row.GetString(1).Trim(),
                Description = row.IsDBNull(2) ? string.Empty : row.GetString(2).Trim()
            };

            return vendor;
        }
        /// <summary>
        /// SSP-137 Reset Password
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static User UserIdMapper(IDataRecord row)
        {
            var user = new User()
            {
                //UserID = row.GetString(0),
                //Email = row.GetString(1)
                UserID = row.IsDBNull(0) ? "" : row.GetString(0),
                Email = row.IsDBNull(1) ? "" : row.GetString(1)
            };
            return user;
        }

        #endregion

    }
}