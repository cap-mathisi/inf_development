using Microsoft.Extensions.Configuration;
using sspx.core.entities;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryAdminPermissionsTests
    {
        private IAdminPermissions _adminPermissions;

        public InMemoryAdminPermissionsTests()
        {
            var dummyConfigForPermissionsOverrideHack = new ConfigurationBuilder().Build();

            _adminPermissions = new InMemoryAdminPermissions(dummyConfigForPermissionsOverrideHack);
        }

        [Fact]
        public void GetAdminMenuPermission()
        {
            int usersKey = 194;
            var expected = AdminMenuPermissionTypes.StaffAdmin;

            var actual = _adminPermissions.GetForUser(usersKey);

            Assert.Equal(expected.ToString(), actual.ToString());
        }
    }
}
