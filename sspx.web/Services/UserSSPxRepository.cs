using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.config;
using sspx.infra.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.web.Services
{
    public class UserSSPxRepository : ISSPxUserRepository
    {
        private ISSPxConfig _config;

        public UserSSPxRepository([FromServices] ISSPxConfig config)
        {
            _config = config;
        }

        public User Add(User user)
        {
            User userToAdd = CleanPhoneNumbers(user);
            return SSPxDBHelper.AddUser(_config.SSPxConnectionString, userToAdd);
        }

        public string Delete(User user)
        {
            return SSPxDBHelper.DeleteUser(_config.SSPxConnectionString, user.UserKey);
        }

        public User GetByKey(int userKey)
        {
            return SSPxDBHelper.GetUser(_config.SSPxConnectionString, userKey);
        }

        public List<User> List()
        {
            throw new NotImplementedException("To be implemented if/when User Admin page is in scope.");
        }

        public string Update(User user)
        {
            User userToUpdate = CleanPhoneNumbers(user);

            return SSPxDBHelper.SaveUser(_config.SSPxConnectionString, userToUpdate);
        }

        #region Helper functions

        private static User CleanPhoneNumbers(User user)
        {
            var cleanedUser = new User(user);
            cleanedUser.CellPhone = digitsOnly(cleanedUser.CellPhone);
            cleanedUser.HomePhone = digitsOnly(cleanedUser.HomePhone);
            cleanedUser.WorkPhone = digitsOnly(cleanedUser.WorkPhone);

            return cleanedUser;
        }

        private static string digitsOnly(string phoneNumberFromUser)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in phoneNumberFromUser)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        #endregion Helper functions
    }
}
