using System.Linq;
using Xunit;
using sspx.core.events;
using sspx.tests.builders;

namespace sspx.tests.core.entities
{
    public class UserAuthenticatedShould
    {
        [Fact]
        public void SetAuthenticatedTrue()
        {
            var user = new UserBuilder().Build();

            var password = "mypass";
            user.Authenticate(password);

            Assert.True(user.Authenticated);
        }

        [Fact]
        public void RaiseUserAuthenticatedEvent()
        {
            var user = new UserBuilder().Build();

            var password = "mypass";
            user.Authenticate(password);

            Assert.Single(user.Events);
            Assert.IsType<UserAuthenticatedEvent>(user.Events.First());
        }
    }
}
