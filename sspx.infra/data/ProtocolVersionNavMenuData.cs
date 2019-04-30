using sspx.core.entities;
using System;

namespace sspx.infra.data
{
    public class ProtocolVersionNavMenuData
    {
        public int ProtocolKey = DefaultValue.Key;
        public int ProtocolVersionKey = DefaultValue.Key;
        public string ProtocolVersion;
        // NAA. Need to standardize on .NET DateTime format <-> smalldatetime
        public DateTime LastUpdatedDate;
    }
}
