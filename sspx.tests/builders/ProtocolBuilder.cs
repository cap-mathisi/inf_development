using sspx.core.entities;

namespace sspx.tests.builders
{
    public class ProtocolBuilder
    {
        private readonly Protocol _protocol = new Protocol();

        public ProtocolBuilder ProtocolId(decimal protocolId)
        {
            _protocol.ProtocolId = protocolId;
            return this;
        }

        public ProtocolBuilder ProtocolName(string protocolName)
        {
            _protocol.ProtocolName = protocolName;
            return this;
        }

        public Protocol Build() => _protocol;
    }
}