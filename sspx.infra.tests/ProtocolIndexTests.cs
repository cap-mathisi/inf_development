using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class ProtocolIndexTests : IClassFixture<SSPDataFixture>
    {
        private IProtocolIndexData _protocolIndexData;
        private SSPDataFixture _fixture;

        public ProtocolIndexTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
            _protocolIndexData = new ProtocolIndexData(
                fixture.SSPxTestConfig
            );
        }

        [Fact]
        public void GetProtocolIndexShouldReturnEmptyListWhenCalledWithNonExistantUser()
        {
            var fakeUserKey = 1;

            var protocolIndex = _protocolIndexData.GetForUser(fakeUserKey);

            Assert.NotNull(protocolIndex);
        }


        // TODO CS2:
        // [Fact]
        //public void GetProtocolIndex()
        //{
        //    var userKey = 1; // TODO: change this

        //    var protocolIndex = _protocolIndexData.GetForUser(userKey);

        //    Assert.True(protocolIndex.AllProtocolsCount > 0, $"Could not find any protocols associated with user key {userKey}.");
        //}
    }
}
