using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.config;
using sspx.infra.data;
using System;
using System.Collections.Generic;

namespace sspx.web.Services
{
    // TODO CS2:
    public class ProtocolRepository : IAdminRepository<Protocol>
    {
        private ISSPxConfig _config;

        public ProtocolRepository([FromServices] ISSPxConfig config)
        {
            _config = config;
        }

        public Protocol Add(Protocol protocol)
        {
            throw new NotImplementedException("moved to ProtocolWithGroupRepository");
        }

        public string Delete(Protocol protocol)
        {
            throw new NotImplementedException();
        }

        public Protocol GetByCkey(decimal cKey)
        {
            throw new NotImplementedException();
        }

        public Protocol GetByKey(int key)
        {
            return SSPxDBHelper.GetProtocol(_config.SSPxConnectionString, key);
        }

        public List<Protocol> List()
        {
            var protocols = SSPxDBHelper.GetProtocols(_config.SSPxConnectionString);
            if (protocols == null)
            {
                protocols = new List<Protocol>();
            }

            return protocols;
        }

        public string Update(Protocol protocol)
        {
            return SSPxDBHelper.SaveProtocol(_config.SSPxConnectionString, protocol);
        }
    }
}
