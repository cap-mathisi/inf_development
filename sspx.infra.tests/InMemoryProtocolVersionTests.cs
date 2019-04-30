using sspx.core.interfaces;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryProtocolVersionTests
    {
        private IProtocolVersionRepository _protocolVersionRepository;

        public InMemoryProtocolVersionTests()
        {
            _protocolVersionRepository = new InMemoryProtocolVersionRepository();
        }

        // TODO CS2
        // [Fact]
        //public void Add()
        //{

        //}

        // TODO CS2
        // [Fact]
        //public void Delete()
        //{

        //}

        [Fact]
        public void GetByKey()
        {
            var expected = "3.2.1.0";

            var version = _protocolVersionRepository.GetByKey(124);
            var actual = version.ProtocolVersionText;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void GetLatestVersionForProtocol()
        //{

        //}

        [Fact]
        public void List()
        {
            var expected = 16;

            var allVersions = _protocolVersionRepository.List();
            var actual = allVersions.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ListForProtocol()
        {
            var expected = 3;

            var versions = _protocolVersionRepository.ListForProtocol(5);
            var actual = versions.Count;

            Assert.Equal(expected, actual);
        }


        // TODO CS2
        // [Fact]
        //public void Update()
        //{

        //}
    }
}
