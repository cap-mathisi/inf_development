using sspx.web.Services;
using System.Linq;
using Xunit;

namespace sspx.infra.tests
{
    public class UserTypeTests : IClassFixture<SSPDataFixture>
    {
        private IUserTypeRepository _userTypeRepository;
        private SSPDataFixture _fixture;

        public UserTypeTests(SSPDataFixture fixture)
        {
            _fixture = fixture;
            _userTypeRepository = new UserTypeRepository(
                _fixture.SSPxTestConfig
            );
        }

        [Fact]
        public void List()
        {
            var expected = "CaCte Member";

            var types = _userTypeRepository.List();
            var actual = types
                .Where(t => t.UserTypeKey == 1)
                .First()
                .Type;

            Assert.Equal(expected, actual);
        }
    }
}
