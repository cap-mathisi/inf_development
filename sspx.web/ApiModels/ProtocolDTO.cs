using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using sspx.core.entities;

namespace sspx.web.ApiModels
{
    public class ProtocolDTO
    {
        [Required]
        public decimal ProtocolId { get; set; }
        public string ProtocolName { get; set; }
        public bool DraftReady { get; private set; } = false;

        public static ProtocolDTO FromProtocol(Protocol protocol)
        {
            return new ProtocolDTO()
            {
                ProtocolId = protocol.ProtocolId,
                ProtocolName = protocol.ProtocolName
            };
        }
    }
}
