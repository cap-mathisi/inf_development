using sspx.core.entities;
using sspx.core.interfaces;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    // TODO CS2:
    public class InMemoryUserTests
    {
        private ISSPxUserRepository _userRepository;

        public InMemoryUserTests()
        {
            _userRepository = new InMemoryUsersSSPxRepository();
        }

        [Fact]
        public void Add()
        {
            int expected = 4;
            var user = new User
            {
                FirstName = "New",
                LastName = "User",
                UserTypeKey = 6
            };

            _userRepository.Add(user);
            var actual = _userRepository.List().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Delete()
        {
            var expected = false;
            var user = new User {
                UserKey = 203
            };

            _userRepository.Delete(user);
            var actual = _userRepository.GetByKey(user.UserKey).Active;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetByKey()
        {
            var expected = "Test Staff";

            var actual = _userRepository.GetByKey(203).FirstName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void List()
        {
            var expected = 3;

            var actual = _userRepository.List().Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update()
        {
            var expected = "Moon Unit";
            var user = new User {
                UserKey = 116,
                FirstName = "Moon Unit",
                LastName = "User",
                UserTypeKey = 5
            };

            _userRepository.Update(user);
            var actual = _userRepository.GetByKey(116).FirstName;

            Assert.Equal(expected, actual);
        }
    }
}
