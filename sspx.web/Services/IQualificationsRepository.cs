using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public interface IQualificationRepository
    {
        Qualification GetByKey(int key);
        List<Qualification> ListActive();
        List<Qualification> List();
        Qualification Add(Qualification qualification);
        string Update(Qualification qualification);
        string Delete(Qualification qualification);
    }
}
