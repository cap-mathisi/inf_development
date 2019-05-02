namespace sspx.core.entities
{
    // <Aggregate>Protocol/ProtocolGroup
    public class ProtocolWithGroup
    {
        public int ProtocolKey = DefaultValue.Key;
        public string ProtocolName = string.Empty;
        public string ProtocolShortName = string.Empty;
        public string ProtocolSortName = string.Empty;
        public bool TestProtocol = false;
        public int CreatedBy = DefaultValue.Key;
        public int LastUpdated = DefaultValue.Key;
        public bool ProtocolActive = DefaultValue.Active;
        public int ProtocolGroupKey = DefaultValue.Key;
        public string ProtocolGroupName = string.Empty;
        public string ProtocolGroupSortName = string.Empty;

        public static ProtocolWithGroup FromProtocol(Protocol protocol, int protocolGroupKey, string protocolGroupName, string protocolGroupSortName)
        {
            return new ProtocolWithGroup
            {
                ProtocolKey = protocol.ProtocolKey,
                ProtocolName = protocol.ProtocolName,
                ProtocolShortName = protocol.ProtocolShortName,
                ProtocolSortName = protocol.ProtocolSortName,
                TestProtocol = protocol.TestProtocol,
                CreatedBy = protocol.CreatedBy,
                LastUpdated = protocol.LastUpdated,
                ProtocolActive = protocol.Active,
                ProtocolGroupKey = protocolGroupKey,
                ProtocolGroupName = protocolGroupName,
                ProtocolGroupSortName = protocolGroupSortName
            };
        }
    }
}
