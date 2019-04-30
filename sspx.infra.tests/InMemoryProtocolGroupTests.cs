using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryProtocolGroupTests
    {
        private IProtocolGroupRepository _protocolGroupRepository;

        public InMemoryProtocolGroupTests()
        {
            _protocolGroupRepository = new InMemoryProtocolGroupRepository();
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
            var expected = "Hematologic";

            var protocolGroup = _protocolGroupRepository.GetByKey(3);
            var actual = protocolGroup.ProtocolGroupName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void List()
        {
            var expected = 14;

            var protocolGroups = _protocolGroupRepository.List();
            var actual = protocolGroups.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ListActive()
        {
            var expected = 13;

            var activeProtocolGroups = _protocolGroupRepository.ListActive();
            var actual = activeProtocolGroups.Count;

            Assert.Equal(expected, actual);
        }

        // TODO CS2
        // [Fact]
        //public void Update()
        //{

        //}
    }
}
