using sspx.core.entities;

namespace sspx.tests.builders
{
    public class UserBuilder
    {
        private readonly User _user = new User();

        public UserBuilder UserId(string userId)
        {
            _user.UserId = userId;
            return this;
        }

        public UserBuilder FirstName(string firstName)
        {
            _user.FirstName = firstName;
            return this;
        }

        public User Build() => _user;
    }
}
