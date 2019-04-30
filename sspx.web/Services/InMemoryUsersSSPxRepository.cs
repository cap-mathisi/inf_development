using sspx.core.entities;
using sspx.core.interfaces;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    public class InMemoryUsersSSPxRepository : ISSPxUserRepository
    {
        private Dictionary<int, User> _users;

        public InMemoryUsersSSPxRepository()
        {
            _users = new Dictionary<int, User>
            {
                // admin
                { 194, new User{ UserKey = 194, FirstName = "Test Admin", LastName = "User", UserTypeKey = 6 }},

                // test
                { 116, new User{ UserKey = 116, FirstName = "Test", LastName = "User", UserTypeKey = 5 }},

                // staff
                { 203, new User{ UserKey = 203, FirstName = "Test Staff", LastName = "User", UserTypeKey = 2 }}
            };
        }

        public User Add(User user)
        {
            int newKey = 1;
            if (_users.Values.Any())
            {
                newKey = _users.Values.Max(u => u.UserKey) + 1;
            }

            user.UserKey = newKey;
            _users.Add(newKey, user);

            return user;
        }

        public string Delete(User user)
        {
            var userToUpdate = _users.Values.First(u => u.UserKey == user.UserKey);
            userToUpdate.Active = false;
            return string.Empty;
        }

        public User GetByKey(int userKey)
        {
            if (userKey == 0)
            {
                return new User();
            }
            return _users.Values.First(u => u.UserKey == userKey);
        }

        public List<User> List()
        {
            return _users.Values.OrderBy(u => u.LastName).ThenBy(u => u.FirstName).ToList();
        }

        public string Update(User user)
        {
            var userToUpdate = _users.Values.First(u => u.UserKey == user.UserKey);

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.MiddleName = user.MiddleName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.WorkPhone = user.WorkPhone;
            userToUpdate.HomePhone = user.HomePhone;
            userToUpdate.CellPhone = user.CellPhone;
            userToUpdate.UserTypeKey = user.UserTypeKey;
            userToUpdate.Qualifications = user.Qualifications;
            userToUpdate.VendorKey = user.VendorKey;
            userToUpdate.Specialties = user.Specialties;

            return string.Empty;
        }
    }
}