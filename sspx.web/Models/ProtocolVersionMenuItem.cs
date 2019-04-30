using sspx.core.entities;
using sspx.infra.data;
using System;

namespace sspx.web.Models
{
    public class ProtocolVersionMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedDate { get; set; }

        public static ProtocolVersionMenuItem FromProtocolVersionNavMenuData(ProtocolVersionNavMenuData protocolVersionNavMenuData)
        {
            return new ProtocolVersionMenuItem
            {
                Id = protocolVersionNavMenuData.ProtocolKey,
                Name = protocolVersionNavMenuData.ProtocolVersion,
                LastUpdatedDate = protocolVersionNavMenuData.LastUpdatedDate
            };
        }

        public static ProtocolVersionMenuItem FromProtocolVersion(ProtocolVersion protocolVersion)
        {
            return new ProtocolVersionMenuItem
            {
                Id = protocolVersion.ProtocolKey,
                Name = protocolVersion.ProtocolVersionText,
                LastUpdatedDate = protocolVersion.LastUpdatedDt
            };
        }
    }
}
