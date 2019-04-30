namespace sspx.core.entities
{
    public class User
    {
        public int UserKey = DefaultValue.Key;
        public string UserID = string.Empty;
        public string FirstName = string.Empty;
        public string MiddleName = string.Empty;
        public string LastName = string.Empty;
        public string Email = string.Empty;
        public string WorkPhone = string.Empty;
        public string HomePhone = string.Empty;
        public string CellPhone = string.Empty;
        public string Password = string.Empty;
        public string UserType = string.Empty;
        public int UserTypeKey = DefaultValue.Key;
        public string Qualifications = string.Empty;
        public int VendorKey = DefaultValue.Key;
        public string Specialties = string.Empty;
        public bool Active = true;

        public User()
        {

        }

        // Copy constructor
        public User(User previousUser)
        {
            UserKey = previousUser.UserKey;
            UserID = previousUser.UserID;
            FirstName = previousUser.FirstName;
            MiddleName = previousUser.MiddleName;
            LastName = previousUser.LastName;
            Email = previousUser.Email;
            WorkPhone = previousUser.WorkPhone;
            HomePhone = previousUser.HomePhone;
            CellPhone = previousUser.CellPhone;
            Password = previousUser.Password;
            UserType = previousUser.UserType;
            UserTypeKey = previousUser.UserTypeKey;
            Qualifications = previousUser.Qualifications;
            VendorKey = previousUser.VendorKey;
            Specialties = previousUser.Specialties;
            Active = previousUser.Active;
        }
    }
}
