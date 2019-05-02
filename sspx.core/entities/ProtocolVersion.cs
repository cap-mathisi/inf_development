using System;

namespace sspx.core.entities
{
    public class ProtocolVersion
    {
        public int ProtocolVersionKey = DefaultValue.Key;
        public int? NamespaceKey = DefaultValue.Namespace;
        public int ProtocolKey = DefaultValue.Key;
        public string ProtocolVersionText = string.Empty;
        public bool TestVersion = false;
        public string Title = string.Empty;
        public string SubTitle = string.Empty;
        // NAA. Need to standardize on .NET DateTime format <-> smalldatetime
        public DateTime? ReleaseDate;
        public DateTime? WebPostingDate;
        public ReleaseStateTypes ReleaseStatesKey;
        public int UserKey;
        public int LastUpdated;
        public DateTime LastUpdatedDt;
        public bool Active = DefaultValue.Active;
        public DateTime? ReviewStartDate;
        public DateTime? ReviewEndDate;
        public string CustomMessage = string.Empty;
    }
}