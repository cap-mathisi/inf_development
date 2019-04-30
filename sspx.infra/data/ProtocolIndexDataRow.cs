using sspx.core.entities;
using System;

namespace sspx.infra.data
{
    public class ProtocolIndexDataRow
    {
        public int ProtocolKey = DefaultValue.Key;
        public string ProtocolName = string.Empty;
        public string ProtocolGroupName = string.Empty;
        public int ProtocolVersionKey = DefaultValue.Key;
        public string ProtocolVersion = string.Empty;
        public int RoleKey = DefaultValue.Key;
        public int CommentsCount = 0;
        public DateTime ProtocolVersionLastUpdatedDt; // NAA. Need to standardize on .NET DateTime format <-> smalldatetime
    }
}
