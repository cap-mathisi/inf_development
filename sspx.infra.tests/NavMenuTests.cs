using Xunit;

namespace sspx.infra.tests
{
    public class NavMenuTests : IClassFixture<SSPDataFixture>
    {
        private SSPDataFixture _fixture;

        public NavMenuTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
        }

        // TODO CS2:
        // [Fact]
        //public void GetProtocolsWithGroupsSql()
        //{

        //}

        // TODO CS2:
        // [Fact]
        //public void GetProtocolVersionsSql()
        //{

        //}
    }
}
