using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryProtocolIndexTests
    {
        private IProtocolIndexData _protocolIndexData;

        public InMemoryProtocolIndexTests()
        {
            _protocolIndexData = new InMemoryProtocolIndexData();
        }

        [Fact]
        public void GetAuthorPanel()
        {
            int expected = 1;
            int fakeUserKey = 194;

            var actual = _protocolIndexData.GetForUser(fakeUserKey).AuthorPanelCount;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetProtocolListLength()
        {
            int expected = 5;
            int fakeUserKey = 194;

            var protocolIndex = _protocolIndexData.GetForUser(fakeUserKey);
            var actual = protocolIndex.Items.Count;

            Assert.Equal(expected, actual);
        }

    }
}
