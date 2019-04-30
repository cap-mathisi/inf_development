using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sspx.core.entities;


namespace sspx.web.Services
{
    public interface IProtocolversionstatus
    {
        List<ProtocolVersionsStates> GetProtocolVersionsStates(int protocolVersionsKey);
    }
}
