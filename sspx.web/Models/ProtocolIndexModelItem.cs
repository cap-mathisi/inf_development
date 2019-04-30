using sspx.core.entities;
using System;
using System.Collections.Generic;

namespace sspx.web.Models
{
    public class ProtocolIndexModelItem
    {
        public int ProtocolKey { get; set; }
        public string ProtocolName { get; set; }
        public string ProtocolGroupName { get; set; }
        public int ProtocolVersionKey { get; set; }
        public string ProtocolVersion { get; set; }
        public List<UserRole> Authors { get; set; }
        public RoleTypes CurrentUserRole { get; set; }
        public int CommentsCount { get; set; }
        public DateTime ProtocolVersionLastUpdatedDt { get; set; }
    }
}
