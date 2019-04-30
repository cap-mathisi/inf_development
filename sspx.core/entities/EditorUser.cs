using System;
using System.Collections.Generic;
using System.Text;

namespace sspx.core.entities
{
    public class EditorUser
    {
        public Int32 userId = 0;
        public string firstName = String.Empty;
        public string middleName = String.Empty;
        public string lastName = String.Empty;
        public string qualification = String.Empty;
        public string role = String.Empty;

        public UserSimple ToUserSimple()
        {
            return new UserSimple
            {
                userId = userId,
                userName = $"{firstName} {middleName} {lastName}, {qualification}",
                searchText = $"{firstName} {middleName} {lastName}, {qualification}".ToLower(),
                role = role
            };
        }
    }

    public class UserSimple
    {
        public Int32 userId = 0;
        public String userName = String.Empty;
        public String role = String.Empty;
        public String searchText = String.Empty;
    }
}
