using Microsoft.AspNetCore.Mvc;
using sspx.core.entities;
using sspx.infra.config;
using sspx.infra.data;
using sspx.web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sspx.web.Services
{
    public class NavMenuData : INavMenuData
    {
        private ISSPxConfig _config;
        private List<ProtocolWithGroup> _protocolsWithGroups;

        public NavMenuData([FromServices] ISSPxConfig config)
        {
            _config = config;
            _protocolsWithGroups = SSPxDBHelper.GetProtocolsWithGroupsActive(_config.SSPxConnectionString);
            _protocolsWithGroups = _protocolsWithGroups ?? new List<ProtocolWithGroup>();
        }

        public Task<List<ProtocolGroupMenuItem>> GetProtocolGroupMenuItems()
        {
            var protocolGroupMenuItems = _protocolsWithGroups
                .GroupBy(m => m.ProtocolGroupKey)
                .Select(m => m.FirstOrDefault())
                .OrderBy(m => m.ProtocolGroupSortName)
                .Select(m => new ProtocolGroupMenuItem(m.ProtocolGroupKey, m.ProtocolGroupName))
                .ToList();

            return Task.FromResult(protocolGroupMenuItems);
        }

        public Task<List<ProtocolMenuItem>> GetProtocolMenuItems(int userKey)
        {
            var userProtocolRoles = SSPxDBHelper.GetUserRolesForAllProtocols(_config.SSPxConnectionString, userKey);
            if(userProtocolRoles == null)
            {
                userProtocolRoles = new List<ProtocolRoleData>();
            }

            var protocolNavMenuData = ProtocolNavMenuData.FromProtocolsWithGroupsAndRoles(_protocolsWithGroups, userProtocolRoles);

            return Task.FromResult(
                protocolNavMenuData
                    .OrderBy(p => p.ProtocolSortName)
                    .Select(p => ProtocolMenuItem.FromNavMenuData(p))
                    .ToList()
                );
        }

        public Task<List<ProtocolVersionMenuItem>> GetProtocolVersions(int protocolKey)
        {
            // TODO CS2:
            var protocolVersions = SSPxDBHelper.GetProtocolVersionsForProtocolViaTemplate(_config.SSPxConnectionString, protocolKey);
            protocolVersions = protocolVersions ?? new List<ProtocolVersion>();

            return Task.FromResult(
                protocolVersions
                    .OrderByDescending(v => v.ProtocolVersionText)
                    .Select(v => ProtocolVersionMenuItem.FromProtocolVersion(v))
                    .ToList()
                );
        }
    }
}
