using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class InMemoryProtocolTests
    {
        private IAdminRepository<Protocol> _protocols;

        public InMemoryProtocolTests()
        {
            _protocols = new InMemoryProtocolRepository();
        }

        [Fact]
        public void Add()
        {
            var expectedCount = 6;
            _protocols.Add(
                new Protocol
                {
                    NamespaceKey = 1,
                    ProtocolName = "Test Protocol",
                    ProtocolShortName = "Test Protocol",
                    ProtocolSortName = "Test Protocol",
                    Active = true
                }
            );
            var protocolsAfterUpdate = _protocols.List();

            var actualCount = protocolsAfterUpdate.Count;

            Assert.Equal(expectedCount, actualCount);
        }

        [Fact]
        public void Delete()
        {
            var expected = false;
            var protocolToDelete = new Protocol
            {
                ProtocolKey = 5,
                NamespaceKey = 1,
                ProtocolName = "Breast Invasive",
                ProtocolShortName = "Breast Invasive",
                ProtocolSortName = "Breast Invasive",
                Active = true
            };

            _protocols.Delete(protocolToDelete);
            var protocolAfterDelete = _protocols.GetByKey(5);
            var actual = protocolAfterDelete.Active;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByKey()
        {
            var expected = "Breast Invasive";

            var protocol = _protocols.GetByKey(5);
            var actual = protocol.ProtocolName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void List()
        {
            var expected = 5;

            var allProtocols = _protocols.List();
            var actual = allProtocols.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update()
        {
            var expected = "Breast Invasive UPDATED NAME";
            var protocol = new Protocol
            {
                ProtocolKey = 5,
                NamespaceKey = 1,
                ProtocolName = "Breast Invasive UPDATED NAME",
                ProtocolShortName = "Breast Invasive UPDATED",
                ProtocolSortName = "Breast Invasive UPDATED",
                Active = false
            };

            _protocols.Update(protocol);
            var actual = _protocols.GetByKey(5).ProtocolName;

            Assert.Equal(expected, actual);
        }
    }
}
