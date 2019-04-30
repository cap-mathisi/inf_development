using System.Collections.Generic;

namespace sspx.core.interfaces
{
    public interface IAdminRepository<T>
    {
        T GetByCkey(decimal cKey);
        T GetByKey(int key);
        List<T> List();
        T Add(T entity);
        string Update(T entity);
        string Delete(T entity);
    }
}
