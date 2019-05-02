namespace sspx.core.entities
{
    public class Protocol
    {
        public int ProtocolKey = DefaultValue.Key;
        public int NamespaceKey = DefaultValue.Namespace;
        public string ProtocolName = string.Empty;
        public string ProtocolShortName = string.Empty;
        public string ProtocolSortName = string.Empty;
        public bool TestProtocol = false;
        public int CreatedBy = DefaultValue.Key;
        public int LastUpdated = DefaultValue.Key;
        public bool Active = DefaultValue.Active;

        public static Protocol FromProtocolWithGroup(ProtocolWithGroup protocolWithGroup)
        {
            return new Protocol
            {
                ProtocolKey = protocolWithGroup.ProtocolKey,
                NamespaceKey = DefaultValue.Namespace,
                ProtocolName = protocolWithGroup.ProtocolName,
                ProtocolShortName = protocolWithGroup.ProtocolShortName,
                ProtocolSortName = protocolWithGroup.ProtocolSortName,
                TestProtocol = protocolWithGroup.TestProtocol,
                CreatedBy = protocolWithGroup.CreatedBy,
                LastUpdated = protocolWithGroup.LastUpdated,
                Active = protocolWithGroup.ProtocolActive
            };
        }
    }
}
