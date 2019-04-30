namespace sspx.core.entities
{
    public class ProtocolGroup
    {
        public int ProtocolGroupKey = DefaultValue.Key;
        public int NamespaceKey = DefaultValue.Namespace;
        public string ProtocolGroupName = string.Empty;
        public string ProtocolGroupSortName = string.Empty;
        public decimal CreatedBy = DefaultValue.Ckey;
        public decimal LastUpdated = DefaultValue.Ckey;
        public bool Active = DefaultValue.Active;
    }
}
