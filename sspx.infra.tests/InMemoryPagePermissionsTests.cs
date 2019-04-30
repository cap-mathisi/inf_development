using Microsoft.Extensions.Configuration;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class InMemoryPagePermissionsTests
    {
        private IPagePermissions _userPageAccess;

        public InMemoryPagePermissionsTests()
        {
            var dummyConfigForPermissionsOverrideHack = new ConfigurationBuilder().Build();

            _userPageAccess = new InMemoryPagePermissions(dummyConfigForPermissionsOverrideHack);
        }

        [Fact]
        public void ShouldNotHavePermisssionToViewPage()
        {
            var expected = false;
            var actual = _userPageAccess.HasPermission(116, "/Protocols/PermissionsDemo");
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldHavePermisssionToViewPage()
        {
            var expected = true;
            var actual = _userPageAccess.HasPermission(203, "/Protocols/PermissionsDemo");
            Assert.Equal(expected, actual);
        }

    }
}
