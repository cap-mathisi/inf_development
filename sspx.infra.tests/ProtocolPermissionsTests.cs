using Microsoft.Extensions.Configuration;
using sspx.core.entities;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class ProtocolPermissionsTests : IClassFixture<SSPDataFixture>
    {
        private IProtocolPermissionRepository _protocolPermissionsRepository;
        private IProtocolPermissions _protocolPermissions;
        private SSPDataFixture _fixture;

        const int TEST_USER_KEY_ALEX_GOEL = 194;
        const int PROTOCOL_KEY_BREAST = 5;
        const int PROTOCOL_KEY_URETHRA = 52;
        const int PROTOCOL_KEY_CNS = 4;

        public ProtocolPermissionsTests(SSPDataFixture fixture)
        {
            var dummyConfigForPermissionsOverrideHack = new ConfigurationBuilder().Build();

            _fixture = fixture;
            _protocolPermissionsRepository = new ProtocolPermissionRepository(fixture.SSPxTestConfig);
            _protocolPermissions = new ProtocolPermissions(
                new ProtocolVersionRepository(fixture.SSPxTestConfig),
                _protocolPermissionsRepository,
                new RoleRepository(fixture.SSPxTestConfig),
                dummyConfigForPermissionsOverrideHack
            );
        }

        [Fact]
        public void AuthorShouldHaveEditProtocolPermission()
        {
            var expected = true;

            var actual = _protocolPermissions.HasPermission(TEST_USER_KEY_ALEX_GOEL, PROTOCOL_KEY_BREAST, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ReviewerShouldNotHaveEditProtocolPermissions()
        {
            var expected = false;

            var actual = _protocolPermissions.HasPermission(TEST_USER_KEY_ALEX_GOEL, PROTOCOL_KEY_URETHRA, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserWithNoRolesForProtocolShouldStillHaveViewPermission()
        {
            var expected = true;

            var actual = _protocolPermissions.HasPermission(TEST_USER_KEY_ALEX_GOEL, PROTOCOL_KEY_CNS, ProtocolPermissionTypes.View);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ListPermissionsForAuthorOfBreastInvasiveProtocol()
        {
            var permissions = _protocolPermissionsRepository.ListForUserProtocol(TEST_USER_KEY_ALEX_GOEL, PROTOCOL_KEY_BREAST);
            var actual = permissions.Count;

            Assert.True(actual > 0);
        }
    }
}
