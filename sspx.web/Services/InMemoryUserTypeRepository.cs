using sspx.core.entities;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Services
{
    // CS2. not implementing the full IAdminRepository<UserType> unless we need a full CRUD repo
    public class InMemoryUserTypeRepository : IUserTypeRepository
    {
        private Dictionary<int, UserType> _userTypes;

        public InMemoryUserTypeRepository()
        {
            _userTypes = new Dictionary<int, UserType>
            {
                {
                    1,
                    new UserType
                    {
                        UserTypeKey = 1,
                        NamespaceKey = 1,
                        Type = "CaCte Member",
                        Description = "Cancer Committee members",
                        SortOrder = 20,
                        Active = true
                    }
                },
                {
                    2,
                    new UserType
                    {
                        UserTypeKey = 2,
                        NamespaceKey = 1,
                        Type = "CaCte Staff",
                        Description = "Cancer Committee  Staff",
                        SortOrder = 40,
                        Active = true
                    }
                },
                {
                    3,
                    new UserType
                    {
                        UserTypeKey = 3,
                        NamespaceKey = 1,
                        Type = "Pert Member",
                        Description = "Pert Members",
                        SortOrder = 30,
                        Active = true
                    }
                },
                {
                    4,
                    new UserType
                    {
                        UserTypeKey = 4,
                        NamespaceKey = 1,
                        Type = "SDT Staff",
                        Description = "Structure Data Team Staff",
                        SortOrder = 50,
                        Active = true
                    }
                },
                {
                    5,
                    new UserType
                    {
                        UserTypeKey = 5,
                        NamespaceKey = 1,
                        Type = "Cancer Protocol Review Panel Member",
                        Description = "Cancer Protocol Review Panel Member",
                        SortOrder = 10,
                        Active = true
                    }
                },
                {
                    6,
                    new UserType
                    {
                        UserTypeKey = 6,
                        NamespaceKey = 1,
                        Type = "Admin",
                        Description = "Admin",
                        SortOrder = 11,
                        Active = true
                    }
                },
                {
                    7,
                    new UserType
                    {
                        UserTypeKey = 7,
                        NamespaceKey = 1,
                        Type = "Super Admin",
                        Description = "Super Admin",
                        SortOrder = 60,
                        Active = true
                    }
                },
            };
        }

        public List<UserType> List()
        {
            return _userTypes.Values.OrderBy(t => t.SortOrder).ToList();
        }
    }
}
