using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.core.interfaces
{
    public interface ISSPxUserRepository
    {
        User GetByKey(int userKey);
        List<User> List();
        User Add(User entity);
        string Update(User entity);
        string Delete(User entity);
    }
}
