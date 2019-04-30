using FluentAssertions;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.data;
using sspx.web.Services;
using Xunit;

namespace sspx.infra.tests
{
    public class UserTests : IClassFixture<UserDataFixture>
    {
        private UserDataFixture _fixture;
        private ISSPxUserRepository _userRepository;

        public UserTests(UserDataFixture fixture)
        {
            _fixture = fixture;
            _userRepository = new UserSSPxRepository(_fixture.SSPxTestConfig);
        }

        [Fact]
        public void AddUser()
        {
            var user = new User()
            {
                UserID = "test_unit_added",
                FirstName = "Unit Test",
                MiddleName = "temporary",
                LastName = "User Added",
                Email = "test_unit_added@cap.org",
                WorkPhone = "",
                HomePhone = "1231231234",
                CellPhone = "5555555555",
                UserTypeKey = 6,
                Qualifications = "Other",
                VendorKey = DefaultValue.Key,
                Specialties = "Breast Pathology, Cytopathology"
            };

            var actual = _userRepository.Add(user);

            Assert.NotNull(actual);
            Assert.NotEqual(actual.UserKey, DefaultValue.Key);
        }

        [Fact]
        public void AddUserAfterCleaningPhoneNumber()
        {
            var expected = "1231231234";

            var user = new User()
            {
                UserID = "test_unit_added_2",
                FirstName = "Unit Test 2",
                MiddleName = "temporary",
                LastName = "User Added 2",
                Email = "test_unit_added@cap.org",
                WorkPhone = "",
                HomePhone = "123-123-1234",
                CellPhone = "555.555.5555",
                UserTypeKey = 6,
                Qualifications = "Other",
                VendorKey = DefaultValue.Key,
                Specialties = "Cytopathology"
            };

            var actual = _userRepository.Add(user).HomePhone;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void DeleteUser()
        {
            var expected = string.Empty;
            // no other tests use this user, so don't have to worry about test run order
            var user = _fixture.Users[0];

            var actual = _userRepository.Delete(user);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetUser()
        {
            var expected = "test_unit_2";
            var user = _fixture.Users[1];

            var retrievedUser = _userRepository.GetByKey(user.UserKey);

            Assert.NotNull(retrievedUser);
            Assert.Equal(expected, retrievedUser.UserID);
        }

        [Fact]
        public void GetUserTwo()
        {
            var expected = new User()
            {
                UserKey = 108,
                UserID = "dmurphy",
                FirstName = "Doug",
                MiddleName = "",
                LastName = "Murphy",
                Email = "dmurphy@cap.org",
                WorkPhone = "          ",
                HomePhone = "          ",
                CellPhone = "          ",
                Qualifications = "MD",
                VendorKey = DefaultValue.Key,
                Specialties = string.Empty
            };

            // NAA.  NOTE:  This is a good example of SSP tight coupling to SSP database and data
            // e.g., FirstName has extra space and we should not have to do below to pass this test.
            var actual = _userRepository.GetByKey(108);
            actual.FirstName = actual.FirstName.TrimEnd();

            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveUser()
        {
            var expected = string.Empty;
            var user = _fixture.Users[1];
            user.LastName = "modified by SaveUser";

            var actual = _userRepository.Update(user);

            Assert.Equal(expected, actual);
        }
    }
}
