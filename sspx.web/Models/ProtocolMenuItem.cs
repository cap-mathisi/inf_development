using sspx.core.entities;
using sspx.infra.data;
using System.Collections.Generic;

namespace sspx.web.Models
{
    public class ProtocolMenuItem
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public List<RoleTypes> UserRoles { get; set; }

        public static ProtocolMenuItem FromNavMenuData(ProtocolNavMenuData navMenuData)
        {
            return new ProtocolMenuItem
            {
                Id = navMenuData.ProtocolKey,
                ParentId = navMenuData.ProtocolGroupKey,
                Name = navMenuData.ProtocolName,
                UserRoles = new List<RoleTypes>(navMenuData.UserRoles)
            };
        }
    }
}
