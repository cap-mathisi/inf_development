using Xunit;
using sspx.infra.data;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Services;

namespace sspx.infra.tests
{
    public class ProtocolTests : IClassFixture<SSPDataFixture>
    {
        private IAdminRepository<Protocol> _protocols;
        SSPDataFixture _fixture;

        public ProtocolTests(SSPDataFixture fixture)
        {
            _fixture = fixture;

            _protocols = new ProtocolRepository(
                fixture.SSPxTestConfig
            );
        }

        // TODO CS2
        // [Fact]
        //public void GetProtocol()
        //{

        //}

        [Fact]
        public void List()
        {
            var expected = "Breast DCIS";

            var allProtocols = _protocols.List();
            var firstProtocol = allProtocols[0];
            var actual = firstProtocol.ProtocolName;

            Assert.Equal(expected, actual);
        }

        // TODO CS2:
        // [Fact]
        //public void GetProtocolsWithGroups()
        //{
        //    var protocolsWithGroups = SSPxDBHelper.GetProtocolsWithGroupsActive(_fixture.SSPxTestConfig.SSPxConnectionString);
        //    var actual = protocolsWithGroups.Count;

        //    Assert.True(actual > 0);

        //    //ProtocolCkey ProtocolName    ProtocolShortName ProtocolSortName    Active ProtocolGroup   ProtocolGroupSortName
        //    //2.100004300 Bone Marrow Bone Marrow NULL    1   Hematologic Hematologic

        //    Assert.True(protocolsWithGroups[0].ProtocolKey == 2);
        //}
    }
}
