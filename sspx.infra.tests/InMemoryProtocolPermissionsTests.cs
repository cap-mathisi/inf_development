using Microsoft.Extensions.Configuration;
using sspx.core.entities;
using sspx.web.Services;
using System.Linq;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryProtocolPermissionsTests
    {
        private IProtocolPermissions _protocolPermissions;
        private IProtocolPermissionRepository _protocolPermissionsRepository;

        const int USER_KEY_NOT_EXISTENT = -1;

        public InMemoryProtocolPermissionsTests()
        {
            var dummyConfigForPermissionsOverrideHack = new ConfigurationBuilder().Build();

            _protocolPermissionsRepository = new InMemoryProtocolPermissionRepository();
            _protocolPermissions = new ProtocolPermissions(
               new InMemoryProtocolVersionRepository(),
               _protocolPermissionsRepository,
               new InMemoryRoleRepository(),
               dummyConfigForPermissionsOverrideHack
            );
        }

        [Fact]
        public void ListForUserProtocol()
        {
            var expected = 2;

            var permissions = _protocolPermissionsRepository.ListForUserProtocol(194, 5);
            var actual = permissions.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AuthorShouldHaveEditProtocolPermission()
        {
            var expected = true;

            var actual = _protocolPermissions.HasPermission(194, 5, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UserWithNoRolesForProtocolShouldStillHaveViewPermission()
        {
            var expected = true;

            var actual = _protocolPermissions.HasPermission(USER_KEY_NOT_EXISTENT, 5, ProtocolPermissionTypes.View);

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ReviewerShouldNotHaveEditProtocolPermissions()
        {
            var expected = false;

            var actual = _protocolPermissions.HasPermission(2, 5, ProtocolPermissionTypes.EditProtocol);

            Assert.Equal(expected, actual);
        }
    }
}
