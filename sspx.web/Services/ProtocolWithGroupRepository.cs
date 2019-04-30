using sspx.core.entities;
using sspx.core.interfaces;
using sspx.infra.config;
using sspx.infra.data;
using System;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class ProtocolWithGroupRepository : IProtocolWithGroupRepository
    {
        private ISSPxConfig _config;

        public ProtocolWithGroupRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public ProtocolWithGroup Add(ProtocolWithGroup protocolWithGroup)
        {
            var protocolToAdd = Protocol.FromProtocolWithGroup(protocolWithGroup);
            
            var newProtocol = SSPxDBHelper.AddProtocol(_config.SSPxConnectionString, protocolToAdd);
            var error = SSPxDBHelper.AddProtocolToGroup(_config.SSPxConnectionString, newProtocol.ProtocolKey, protocolWithGroup.ProtocolGroupKey);

            if (error != string.Empty)
            {
                // TODO CS2:
                throw new Exception($"Could not add Protocol to Protocol Group: {error}");
            }

            var newProtocolWithGroup = ProtocolWithGroup.FromProtocol(
                newProtocol,
                protocolWithGroup.ProtocolGroupKey,
                protocolWithGroup.ProtocolGroupName,
                protocolWithGroup.ProtocolGroupSortName
            );
            return newProtocolWithGroup;
        }

        public string Delete(ProtocolWithGroup protocolWithGroup)
        {
            throw new NotImplementedException();
        }

        public ProtocolWithGroup GetByKey(int key)
        {
            return SSPxDBHelper.GetProtocolWithGroup(_config.SSPxConnectionString, key);
        }

        public List<ProtocolWithGroup> ListActive()
        {
            return SSPxDBHelper.GetProtocolsWithGroupsActive(_config.SSPxConnectionString);
        }

        public List<ProtocolWithGroup> List()
        {
            return SSPxDBHelper.GetProtocolsWithGroups(_config.SSPxConnectionString);
        }

        public string Update(ProtocolWithGroup protocolWithGroup)
        {
            // TODO CS2:
            throw new NotImplementedException();
        }
    }
}
