using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public interface IProtocolGroupRepository
    {
        ProtocolGroup GetByKey(int key);
        List<ProtocolGroup> ListActive();
        List<ProtocolGroup> List();
        ProtocolGroup Add(ProtocolGroup ProtocolGroup);
        string Update(ProtocolGroup ProtocolGroup);
        string Delete(ProtocolGroup ProtocolGroup);
    }
}
