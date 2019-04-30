using sspx.core.entities;
using sspx.infra.data;
using System.Collections.Generic;
using System.Linq;

namespace sspx.web.Models
{
    public class ProtocolRoles
    {
        public int ProtocolKey { get; set; }
        public List<RoleTypes> Roles { get; set; }

        public static List<ProtocolRoles> FromProtocolRoleData(List<ProtocolRoleData> protocolRoleDataList)
        {
            var protocolRolesList = new List<ProtocolRoles>();

            var query = protocolRoleDataList.GroupBy(d => d.ProtocolKey);
            foreach(IGrouping<int, ProtocolRoleData> grouping in query)
            {
                var rolesForThisProtocol = new List<RoleTypes>();
                foreach (ProtocolRoleData protocolRoleData in grouping)
                {
                    rolesForThisProtocol.Add((RoleTypes)protocolRoleData.RoleKey);
                }

                protocolRolesList.Add(new ProtocolRoles
                {
                    ProtocolKey = grouping.Key,
                    Roles = rolesForThisProtocol
                });
            }

            return protocolRolesList;
        }
    }
}
