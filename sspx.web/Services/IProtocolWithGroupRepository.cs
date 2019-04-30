using sspx.core.entities;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public interface IProtocolWithGroupRepository
    {
        ProtocolWithGroup GetByKey(int key);
        List<ProtocolWithGroup> ListActive();
        List<ProtocolWithGroup> List();
        ProtocolWithGroup Add(ProtocolWithGroup protocolWithGroup);
        string Update(ProtocolWithGroup protocolWithGroup);
        string Delete(ProtocolWithGroup protocolWithGroup);
    }
}
