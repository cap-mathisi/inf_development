
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.config;
using sspx.infra.data;
using sspx.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sspx.web.Services
{
    public class Protocolversionstatus : IProtocolversionstatus
    {
        private ISSPxConfig _config;

        public Protocolversionstatus(ISSPxConfig config)
        {
            _config = config;
        }

        public List<ProtocolVersionsStates> GetProtocolVersionsStates(int protocolVersionsKey)
        {
            return SSPxDBHelper.GetProtocolVersionsStates(_config.SSPxConnectionString, protocolVersionsKey);
        }
    }
}
