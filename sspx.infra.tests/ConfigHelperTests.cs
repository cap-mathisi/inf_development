using sspx.web.Helpers;
using System;
using Xunit;

namespace sspx.infra.tests
{
    public class ConfigHelperTests
    {

        [Fact]
        public void GivenNegativeLockoutSettingShouldDefaultToThree()
        {
            var expected = 3;

            var actual = ConfigHelper.GetLockoutMaxFailedAttempts("-10");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNoLockoutSettingShouldDefaultToThree()
        {
            var expected = 3;

            var actual = ConfigHelper.GetLockoutMaxFailedAttempts(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenLockoutSettingOfFiveShouldReturnFive()
        {
            var expected = 5;

            var actual = ConfigHelper.GetLockoutMaxFailedAttempts("5");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNoTimeoutSettingIdentityTimeoutShouldBeTwentyMinutes()
        {
            var expected = TimeSpan.FromMinutes(20);

            var actual = ConfigHelper.GetIdentityTimeout(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenNoTimeoutSettingSessionTimeoutShouldBeTwentyFiveMinutes()
        {
            var expected = TimeSpan.FromMinutes(25);

            var actual = ConfigHelper.GetSessionTimeout(null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenZeroMinutesTimeOutSettingIdentityShouldReturnTwentyMinutes()
        {
            var expected = TimeSpan.FromMinutes(20);

            var actual = ConfigHelper.GetIdentityTimeout("0");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenTenMinutesTimeOutSettingIdentityShouldReturnTenMinutes()
        {
            var expected = TimeSpan.FromMinutes(10);

            var actual = ConfigHelper.GetIdentityTimeout("10");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenTenMinutesTimeOutSettingSessionShouldReturnFifteenMinutes()
        {
            var expected = TimeSpan.FromMinutes(15);

            var actual = ConfigHelper.GetSessionTimeout("10");

            Assert.Equal(expected, actual);
        }

    }
}
