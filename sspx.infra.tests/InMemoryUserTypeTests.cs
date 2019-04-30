using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryUserTypeTests
    {
        private IUserTypeRepository _userTypeRepository;

        public InMemoryUserTypeTests()
        {
            _userTypeRepository = new InMemoryUserTypeRepository();

        }

        [Fact]
        public void List()
        {
            var expected = 7;

            var allTypes = _userTypeRepository.List();
            var actual = allTypes.Count;

            Assert.Equal(expected, actual);
        }
    }
}
