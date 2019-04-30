using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class ProtocolVersionUserRole
    {
        public int ProtocolVersionUserRoleKey = DefaultValue.Key;
        public int RolesKey = DefaultValue.Key;
        public int UserKey;
        public int ProtocolVersionsKey;
        public string AuthorListFunction = string.Empty;
        public string Comments = string.Empty;
        public decimal CreatedBy;
        public DateTime? CreatedDt;
        public decimal? LastUpdated;
        public DateTime? LastUpdatedDt;
        public bool? Assignreviewer_flag;
    }
}
