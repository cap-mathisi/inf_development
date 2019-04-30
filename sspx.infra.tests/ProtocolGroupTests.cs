using sspx.infra.data;
using Xunit;

namespace sspx.infra.tests
{
    public class ProtocolGroupTests : IClassFixture<SSPDataFixture>
    {
        private SSPDataFixture _fixture;

        public ProtocolGroupTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
        }

        // TODO CS2
        // [Fact]
        //public void GetProtocolGroup()
        //{

        //}

        [Fact]
        public void GetProtocolGroups()
        {
            var actual = SSPxDBHelper.GetProtocolGroups(_fixture.SSPxTestConfig.SSPxConnectionString);

            Assert.NotNull(actual);
            Assert.True(actual.Count > 0);
        }

        // TODO CS2
        // [Fact]
        //public void AddProtocolGroup()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void DeleteProtocolGroup()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void SaveProtocolGroup()
        //{

        //}
    }
}
