using Xunit;
using sspx.infra.data;

namespace sspx.infra.tests
{
    public class ProtocolVersionTests : IClassFixture<SSPDataFixture>
    {
        SSPDataFixture _fixture;

        public ProtocolVersionTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
        }

        // TODO CS2:
        // [Fact]
        //public void GetProtocolVersions()
        //{
        //    var protocolVersions = SSPxDBHelper.GetProtocolVersions(_fixture.SSPxTestConfig.SSPxConnectionString);
        //    var actual = protocolVersions.Count;

        //    Assert.True(actual > 0);
        //}

        // TODO CS2:
        // [Fact]
        //public void GetProtocolVersionsForUser()
        //{
        //    decimal userCkey = 113.100004300m;
        //    var protocolVersions = SSPxDBHelper.GetProtocolVersionsForUser(_fixture.SSPxTestConfig.SSPxConnectionString, userCkey);
        //    var actual = protocolVersions.Count;

        //    Assert.True(actual > 0);
        //}

        // TODO CS2:
        // [Fact]
        //public void GetUserRolesForProtocolVersion()
        //{
        //    var userCkey = 109.100004300m;
        //    var protocolVersionCkey = 198.100004300m;
        //    var actual = SSPxDBHelper.GetUserRolesForProtocolVersion(_fixture.SSPxTestConfig.SSPxConnectionString, userCkey, protocolVersionCkey);

        //    Assert.True(actual.Count > 0);
        //}
    }
}
