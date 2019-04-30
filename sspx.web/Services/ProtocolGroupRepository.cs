using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;
using System;
using System.Collections.Generic;

namespace sspx.web.Services
{
    public class ProtocolGroupRepository : IProtocolGroupRepository
    {
        private ISSPxConfig _config;

        public ProtocolGroupRepository(ISSPxConfig config)
        {
            _config = config;
        }

        public ProtocolGroup Add(ProtocolGroup protocolGroup)
        {
            return SSPxDBHelper.AddProtocolGroup(_config.SSPxConnectionString, protocolGroup);
        }

        public string Delete(ProtocolGroup protocolGroup)
        {
            return SSPxDBHelper.DeleteProtocolGroup(_config.SSPxConnectionString, protocolGroup.ProtocolGroupKey, protocolGroup.LastUpdated);
        }

        // TODO CS2:
        public ProtocolGroup GetByCkey(decimal cKey)
        {
            throw new NotImplementedException();
        }

        public ProtocolGroup GetByKey(int key)
        {
            return SSPxDBHelper.GetProtocolGroup(_config.SSPxConnectionString, key);
        }

        public List<ProtocolGroup> List()
        {
            var protocolGroups = SSPxDBHelper.GetProtocolGroups(_config.SSPxConnectionString);
            return protocolGroups ?? new List<ProtocolGroup>();
        }

        public List<ProtocolGroup> ListActive()
        {
            var protocolGroups = SSPxDBHelper.GetProtocolGroupsActive(_config.SSPxConnectionString);
            return protocolGroups ?? new List<ProtocolGroup>();
        }

        public string Update(ProtocolGroup protocolGroup)
        {
            return SSPxDBHelper.SaveProtocolGroup(_config.SSPxConnectionString, protocolGroup);
        }
    }
}
